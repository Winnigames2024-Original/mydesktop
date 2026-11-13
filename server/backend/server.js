'use strict';
/*
 * MyDesktop backend — port 2450
 * ПОЛНОСТЬЮ ОТДЕЛЬНЫЙ от NareLauncher. Ничего общего с narenet/nare-rtmp.
 *
 * Отвечает за:
 *   - аккаунты (scrypt-хеши), общие с веб-частью: /api/register /api/login /api/delete /api/accounts
 *   - автообновление приложения: /update.json  (version + sha1 + size + url)
 *   - раздачу самого exe: /download/MyDesktop.exe
 *   - статику веба из WEB_DIR
 *
 * nginx проксирует  location /mydesktop/  ->  http://127.0.0.1:2450/  (со слэшем,
 * т.е. префикс /mydesktop/ срезается, сюда приходят пути вида /api/login, /update.json).
 *
 * Только стандартная библиотека Node — никаких npm-зависимостей, чтобы бэкенд не падал
 * из-за окружения и ставился одной командой.
 */

const http = require('http');
const fs = require('fs');
const path = require('path');
const crypto = require('crypto');

const PORT = parseInt(process.env.MYDESKTOP_PORT || '2450', 10);
const DATA_DIR = process.env.MYDESKTOP_DATA || '/opt/mydesktop/data';
const WEB_DIR = process.env.MYDESKTOP_WEB || '/var/www/mydesktop';
const APP_DIR = process.env.MYDESKTOP_APP || '/opt/mydesktop/app'; // тут лежит MyDesktop.exe + update.json
const ACCOUNTS_FILE = path.join(DATA_DIR, 'accounts.json');

// ───────────────────────── scrypt ─────────────────────────
// Формат хранения хеша:  scrypt$<N>$<r>$<p>$<saltB64>$<hashB64>
const SCRYPT = { N: 16384, r: 8, p: 1, keylen: 64 };

function hashPassword(password) {
  const salt = crypto.randomBytes(16);
  const dk = crypto.scryptSync(password, salt, SCRYPT.keylen, { N: SCRYPT.N, r: SCRYPT.r, p: SCRYPT.p });
  return `scrypt$${SCRYPT.N}$${SCRYPT.r}$${SCRYPT.p}$${salt.toString('base64')}$${dk.toString('base64')}`;
}

function verifyPassword(password, stored) {
  try {
    const parts = String(stored).split('$');
    if (parts.length !== 6 || parts[0] !== 'scrypt') return false;
    const N = parseInt(parts[1], 10), r = parseInt(parts[2], 10), p = parseInt(parts[3], 10);
    const salt = Buffer.from(parts[4], 'base64');
    const expected = Buffer.from(parts[5], 'base64');
    const dk = crypto.scryptSync(password, salt, expected.length, { N, r, p });
    return dk.length === expected.length && crypto.timingSafeEqual(dk, expected);
  } catch (_) {
    return false;
  }
}

// ───────────────────────── хранилище аккаунтов ─────────────────────────
function ensureDirs() {
  for (const d of [DATA_DIR, APP_DIR]) {
    try { fs.mkdirSync(d, { recursive: true }); } catch (_) {}
  }
  if (!fs.existsSync(ACCOUNTS_FILE)) {
    fs.writeFileSync(ACCOUNTS_FILE, JSON.stringify({ accounts: [] }, null, 2));
  }
}

function loadAccounts() {
  try {
    const j = JSON.parse(fs.readFileSync(ACCOUNTS_FILE, 'utf8'));
    return Array.isArray(j.accounts) ? j.accounts : [];
  } catch (_) {
    return [];
  }
}

function saveAccounts(list) {
  const tmp = ACCOUNTS_FILE + '.tmp';
  fs.writeFileSync(tmp, JSON.stringify({ accounts: list }, null, 2));
  fs.renameSync(tmp, ACCOUNTS_FILE); // атомарно
}

function findAccount(list, login) {
  const key = String(login).trim().toLowerCase();
  return list.find(a => String(a.login).toLowerCase() === key);
}

const LOGIN_RE = /^[A-Za-z0-9_.\-]{3,32}$/;

// ───────────────────────── helpers ─────────────────────────
function send(res, code, obj, headers) {
  const body = Buffer.from(JSON.stringify(obj));
  res.writeHead(code, Object.assign({
    'Content-Type': 'application/json; charset=utf-8',
    'Content-Length': body.length,
    'Cache-Control': 'no-store'
  }, headers || {}));
  res.end(body);
}

function readJsonBody(req, limit = 64 * 1024) {
  return new Promise((resolve, reject) => {
    let size = 0; const chunks = [];
    req.on('data', c => {
      size += c.length;
      if (size > limit) { reject(new Error('too large')); req.destroy(); return; }
      chunks.push(c);
    });
    req.on('end', () => {
      if (!chunks.length) return resolve({});
      try { resolve(JSON.parse(Buffer.concat(chunks).toString('utf8'))); }
      catch (_) { reject(new Error('bad json')); }
    });
    req.on('error', reject);
  });
}

// публичное представление аккаунта (без хеша!)
function publicAccount(a) {
  return { login: a.login, created: a.created, admin: !!a.admin };
}

// ───────────────────────── update.json ─────────────────────────
// Отдаём файл update.json из APP_DIR, если он есть; иначе считаем на лету из exe.
function computeUpdateInfo() {
  const exePath = path.join(APP_DIR, 'MyDesktop.exe');
  const jsonPath = path.join(APP_DIR, 'update.json');
  if (fs.existsSync(jsonPath)) {
    try { return JSON.parse(fs.readFileSync(jsonPath, 'utf8')); } catch (_) {}
  }
  if (fs.existsSync(exePath)) {
    const buf = fs.readFileSync(exePath);
    const sha1 = crypto.createHash('sha1').update(buf).digest('hex');
    return { version: '0.0.0', sha1, size: buf.length, url: '/mydesktop/download/MyDesktop.exe' };
  }
  return { version: '0.0.0', sha1: '', size: 0, url: '/mydesktop/download/MyDesktop.exe' };
}

// ───────────────────────── статика ─────────────────────────
const MIME = {
  '.html': 'text/html; charset=utf-8', '.js': 'text/javascript; charset=utf-8',
  '.css': 'text/css; charset=utf-8', '.json': 'application/json; charset=utf-8',
  '.png': 'image/png', '.jpg': 'image/jpeg', '.svg': 'image/svg+xml', '.ico': 'image/x-icon'
};

function serveStatic(res, urlPath) {
  let rel = decodeURIComponent(urlPath.split('?')[0]);
  if (rel === '/' || rel === '') rel = '/index.html';
  const full = path.normalize(path.join(WEB_DIR, rel));
  if (!full.startsWith(path.normalize(WEB_DIR))) { send(res, 403, { error: 'forbidden' }); return; }
  fs.readFile(full, (err, buf) => {
    if (err) { send(res, 404, { error: 'not found' }); return; }
    res.writeHead(200, { 'Content-Type': MIME[path.extname(full).toLowerCase()] || 'application/octet-stream', 'Content-Length': buf.length });
    res.end(buf);
  });
}

function serveExe(res) {
  const exePath = path.join(APP_DIR, 'MyDesktop.exe');
  fs.readFile(exePath, (err, buf) => {
    if (err) { send(res, 404, { error: 'exe not found' }); return; }
    res.writeHead(200, {
      'Content-Type': 'application/octet-stream',
      'Content-Length': buf.length,
      'Content-Disposition': 'attachment; filename="MyDesktop.exe"'
    });
    res.end(buf);
  });
}

// ───────────────────────── роутер ─────────────────────────
const server = http.createServer(async (req, res) => {
  const url = req.url || '/';
  const pathname = url.split('?')[0];
  try {
    // --- API ---
    if (pathname === '/api/health') return send(res, 200, { ok: true, service: 'mydesktop-backend', port: PORT });

    if (pathname === '/update.json') return send(res, 200, computeUpdateInfo());
    if (pathname === '/download/MyDesktop.exe') return serveExe(res);

    if (pathname === '/api/register' && req.method === 'POST') {
      const b = await readJsonBody(req);
      const login = String(b.login || '').trim();
      const password = String(b.password || '');
      if (!LOGIN_RE.test(login)) return send(res, 400, { error: 'bad_login', message: 'Логин: 3-32 симв., буквы/цифры/._-' });
      if (password.length < 6) return send(res, 400, { error: 'bad_password', message: 'Пароль минимум 6 символов' });
      const list = loadAccounts();
      if (findAccount(list, login)) return send(res, 409, { error: 'exists', message: 'Такой логин уже есть' });
      const acc = { login, hash: hashPassword(password), created: new Date().toISOString(), admin: list.length === 0 };
      list.push(acc);
      saveAccounts(list);
      return send(res, 201, { ok: true, account: publicAccount(acc) });
    }

    if (pathname === '/api/login' && req.method === 'POST') {
      const b = await readJsonBody(req);
      const list = loadAccounts();
      const acc = findAccount(list, b.login);
      if (!acc || !verifyPassword(String(b.password || ''), acc.hash)) {
        return send(res, 401, { error: 'invalid', message: 'Неверный логин или пароль' });
      }
      return send(res, 200, { ok: true, account: publicAccount(acc) });
    }

    if (pathname === '/api/delete' && req.method === 'POST') {
      // удаление своего аккаунта по паролю
      const b = await readJsonBody(req);
      const list = loadAccounts();
      const acc = findAccount(list, b.login);
      if (!acc || !verifyPassword(String(b.password || ''), acc.hash)) {
        return send(res, 401, { error: 'invalid', message: 'Неверный логин или пароль' });
      }
      const next = list.filter(a => a !== acc);
      saveAccounts(next);
      return send(res, 200, { ok: true });
    }

    if (pathname === '/api/accounts' && req.method === 'GET') {
      // список логинов без хешей
      return send(res, 200, { accounts: loadAccounts().map(publicAccount) });
    }

    if (pathname === '/api/count' && req.method === 'GET') {
      return send(res, 200, { count: loadAccounts().length });
    }

    // --- статика веба ---
    if (req.method === 'GET') return serveStatic(res, pathname);

    return send(res, 404, { error: 'not_found' });
  } catch (e) {
    return send(res, 400, { error: 'bad_request', message: String(e && e.message || e) });
  }
});

ensureDirs();
server.listen(PORT, '127.0.0.1', () => {
  console.log(`[mydesktop-backend] listening on 127.0.0.1:${PORT}  data=${DATA_DIR} web=${WEB_DIR} app=${APP_DIR}`);
});
