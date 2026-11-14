#!/usr/bin/env bash
# =============================================================================
#  MyDesktop — установщик/обновлятор серверной части (идемпотентный)
#  Запуск повторно = обновление. НИЧЕГО из NareLauncher не трогает.
#
#  Одна команда для пользователя (один раз):
#    curl -fsSL https://raw.githubusercontent.com/<OWNER>/mydesktop/main/setup.sh | bash
#
#  Порты MyDesktop (НЕ пересекаются с NareLauncher 2445/1935/8000/2444):
#    backend   2450
#    RTMP      1936     HLS-HTTP 8001
#    RustDesk  hbbs 21115, 21116(+udp)    hbbr 21117
#
#  Что НЕ трогаем: /opt/narelauncher-mods, /var/www/narelauncher,
#    сервисы narenet / nare-rtmp, их update.sh и таймеры.
# =============================================================================
set -euo pipefail

# ─────────────────────────── настройки (меняются здесь) ───────────────────────────
# Подхватываем сохранённые настройки (чтобы таймер/update.sh работали без env).
[ -f /opt/mydesktop/mydesktop.env ] && . /opt/mydesktop/mydesktop.env || true

OWNER="${MYDESKTOP_OWNER:-Winnigames2024-Original}"   # GitHub owner, где лежит репозиторий mydesktop
REPO="${MYDESKTOP_REPO:-mydesktop}"
BRANCH="${MYDESKTOP_BRANCH:-main}"
REPO_URL="${MYDESKTOP_REPO_URL:-https://github.com/${OWNER}/${REPO}.git}"
RAW_URL="https://raw.githubusercontent.com/${OWNER}/${REPO}/${BRANCH}"

VPS_IP="${MYDESKTOP_IP:-151.245.217.104}"

BACKEND_PORT=2450
WEB_PORT=2451          # отдельный nginx-порт MyDesktop (свой server-блок, чужих сайтов не трогает)
RTMP_PORT=1936
HLS_PORT=8001
HBBS_PORT=21116        # основной порт сигнального сервера RustDesk
HBBR_PORT=21117        # релей RustDesk

BASE=/opt/mydesktop
REPO_DIR="$BASE/repo"
WEB_DIR=/var/www/mydesktop
DATA_DIR="$BASE/data"
APP_DIR="$BASE/app"            # сюда кладётся MyDesktop.exe + update.json
MEDIA_DIR="$BASE/media"
RUSTDESK_DIR="$BASE/rustdesk"
RUSTDESK_VER="${MYDESKTOP_RUSTDESK_VER:-1.1.14}"

NGINX_SNIPPET=/etc/nginx/snippets/mydesktop.conf
NGINX_SITE="${MYDESKTOP_NGINX_SITE:-}"         # путь к server-блоку, КУДА вставить include (опционально)

log(){ printf '\033[1;36m[mydesktop]\033[0m %s\n' "$*"; }
warn(){ printf '\033[1;33m[mydesktop] ВНИМАНИЕ:\033[0m %s\n' "$*"; }
die(){ printf '\033[1;31m[mydesktop] ОШИБКА:\033[0m %s\n' "$*" >&2; exit 1; }

[ "$(id -u)" = "0" ] || die "Запускать от root (sudo)."

# ─────────────────────────── guard: не затронуть NareLauncher ───────────────────────────
# Любой из путей/портов MyDesktop не должен совпасть с NareLauncher.
for p in "$BASE" "$WEB_DIR"; do
  case "$p" in
    /opt/narelauncher-mods*|/var/www/narelauncher*) die "Путь $p пересекается с NareLauncher — остановлено." ;;
  esac
done
for port in "$BACKEND_PORT" "$RTMP_PORT" "$HLS_PORT"; do
  case "$port" in 2445|1935|8000|2444) die "Порт $port принадлежит NareLauncher — остановлено." ;; esac
done

# ─────────────────────────── 1. пакеты ───────────────────────────
install_packages(){
  log "Проверка пакетов (node, npm, ffmpeg, git, nginx, curl, unzip)…"
  export DEBIAN_FRONTEND=noninteractive
  local need=()
  command -v git  >/dev/null || need+=(git)
  command -v curl >/dev/null || need+=(curl)
  command -v unzip>/dev/null || need+=(unzip)
  command -v ffmpeg>/dev/null || need+=(ffmpeg)
  command -v nginx >/dev/null || need+=(nginx)
  if [ "${#need[@]}" -gt 0 ]; then
    apt-get update -y
    apt-get install -y "${need[@]}"
  fi
  # Node.js 20 LTS, если нет или слишком старый
  if ! command -v node >/dev/null || [ "$(node -p 'process.versions.node.split(".")[0]' 2>/dev/null || echo 0)" -lt 18 ]; then
    log "Ставлю Node.js 20 LTS…"
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash -
    apt-get install -y nodejs
  fi
  log "node $(node --version), ffmpeg $(ffmpeg -version | head -1 | awk '{print $3}')"
}

# ─────────────────────────── 2. репозиторий ───────────────────────────
sync_repo(){
  [ "$OWNER" != "CHANGE_ME" ] || die "Укажите OWNER (ваш GitHub) в setup.sh или через MYDESKTOP_OWNER."
  mkdir -p "$BASE"
  # сохраняем настройки, чтобы таймер/update.sh подхватывали их без env
  cat >"$BASE/mydesktop.env" <<EOF
MYDESKTOP_OWNER=$OWNER
MYDESKTOP_REPO=$REPO
MYDESKTOP_BRANCH=$BRANCH
MYDESKTOP_IP=$VPS_IP
MYDESKTOP_NGINX_SITE=$NGINX_SITE
EOF
  if [ -d "$REPO_DIR/.git" ]; then
    log "Обновляю репозиторий…"
    git -C "$REPO_DIR" fetch --depth 1 origin "$BRANCH"
    git -C "$REPO_DIR" reset --hard "origin/$BRANCH"
  else
    log "Клонирую $REPO_URL…"
    git clone --depth 1 -b "$BRANCH" "$REPO_URL" "$REPO_DIR"
  fi
}

# ─────────────────────────── 3. каталоги + деплой ───────────────────────────
deploy_files(){
  mkdir -p "$WEB_DIR" "$DATA_DIR" "$APP_DIR" "$MEDIA_DIR" "$RUSTDESK_DIR"
  log "Копирую бэкенд/rtmp/web…"
  cp -f "$REPO_DIR/server/backend/server.js"  "$BASE/server.js"
  cp -f "$REPO_DIR/server/backend/package.json" "$BASE/backend-package.json"
  mkdir -p "$BASE/rtmp"
  cp -f "$REPO_DIR/server/rtmp/rtmp.js"       "$BASE/rtmp/rtmp.js"
  cp -f "$REPO_DIR/server/rtmp/package.json"  "$BASE/rtmp/package.json"
  # web — только внутрь /var/www/mydesktop
  rsync -a --delete "$REPO_DIR/server/web/" "$WEB_DIR/" 2>/dev/null || cp -rf "$REPO_DIR/server/web/." "$WEB_DIR/"
  # exe + update.json из репо, если они там есть (кладёт сборка приложения)
  [ -f "$REPO_DIR/app/MyDesktop.exe" ] && cp -f "$REPO_DIR/app/MyDesktop.exe" "$APP_DIR/MyDesktop.exe" || true
  [ -f "$REPO_DIR/update.json" ] && cp -f "$REPO_DIR/update.json" "$APP_DIR/update.json" || true
  # зависимости rtmp
  log "npm install для rtmp…"
  (cd "$BASE/rtmp" && npm install --omit=dev --no-audit --no-fund)
}

# ─────────────────────────── 4. RustDesk (hbbs/hbbr) ───────────────────────────
install_rustdesk(){
  local zip="$RUSTDESK_DIR/rustdesk-server.zip"
  if [ ! -x "$RUSTDESK_DIR/hbbs" ]; then
    log "Скачиваю RustDesk server $RUSTDESK_VER…"
    curl -fsSL -o "$zip" \
      "https://github.com/rustdesk/rustdesk-server/releases/download/${RUSTDESK_VER}/rustdesk-server-linux-amd64.zip"
    unzip -o "$zip" -d "$RUSTDESK_DIR" >/dev/null
    # бинарники могут лежать в подпапке amd64/
    [ -f "$RUSTDESK_DIR/amd64/hbbs" ] && cp -f "$RUSTDESK_DIR/amd64/"* "$RUSTDESK_DIR/" || true
    chmod +x "$RUSTDESK_DIR/hbbs" "$RUSTDESK_DIR/hbbr" 2>/dev/null || true
    rm -f "$zip"
  else
    log "RustDesk server уже установлен."
  fi
}

# ─────────────────────────── 5. systemd-сервисы ───────────────────────────
write_services(){
  log "Пишу systemd-юниты (mydesktop, mydesktop-rtmp, mydesktop-hbbs, mydesktop-hbbr)…"

  cat >/etc/systemd/system/mydesktop.service <<EOF
[Unit]
Description=MyDesktop backend (accounts + update.json) :$BACKEND_PORT
After=network.target
[Service]
Environment=MYDESKTOP_PORT=$BACKEND_PORT
Environment=MYDESKTOP_DATA=$DATA_DIR
Environment=MYDESKTOP_WEB=$WEB_DIR
Environment=MYDESKTOP_APP=$APP_DIR
ExecStart=$(command -v node) $BASE/server.js
Restart=always
RestartSec=3
[Install]
WantedBy=multi-user.target
EOF

  cat >/etc/systemd/system/mydesktop-rtmp.service <<EOF
[Unit]
Description=MyDesktop RTMP/HLS media server :$RTMP_PORT/$HLS_PORT
After=network.target
[Service]
WorkingDirectory=$BASE/rtmp
Environment=MYDESKTOP_RTMP_PORT=$RTMP_PORT
Environment=MYDESKTOP_HLS_PORT=$HLS_PORT
Environment=MYDESKTOP_MEDIAROOT=$MEDIA_DIR
Environment=MYDESKTOP_FFMPEG=$(command -v ffmpeg)
ExecStart=$(command -v node) $BASE/rtmp/rtmp.js
Restart=always
RestartSec=3
[Install]
WantedBy=multi-user.target
EOF

  cat >/etc/systemd/system/mydesktop-hbbs.service <<EOF
[Unit]
Description=MyDesktop RustDesk ID server (hbbs)
After=network.target
[Service]
WorkingDirectory=$RUSTDESK_DIR
ExecStart=$RUSTDESK_DIR/hbbs -r $VPS_IP:$HBBR_PORT
Restart=always
RestartSec=3
[Install]
WantedBy=multi-user.target
EOF

  cat >/etc/systemd/system/mydesktop-hbbr.service <<EOF
[Unit]
Description=MyDesktop RustDesk relay server (hbbr)
After=network.target
[Service]
WorkingDirectory=$RUSTDESK_DIR
ExecStart=$RUSTDESK_DIR/hbbr
Restart=always
RestartSec=3
[Install]
WantedBy=multi-user.target
EOF

  systemctl daemon-reload
  systemctl enable --now mydesktop.service mydesktop-rtmp.service mydesktop-hbbs.service mydesktop-hbbr.service
  systemctl restart mydesktop.service mydesktop-rtmp.service
}

# ─────────────────────────── 6. таймер автообновления ───────────────────────────
write_update_timer(){
  log "Настраиваю таймер автообновления (git pull + update.sh)…"
  cp -f "$REPO_DIR/update.sh" "$BASE/update.sh"
  chmod +x "$BASE/update.sh"

  cat >/etc/systemd/system/mydesktop-update.service <<EOF
[Unit]
Description=MyDesktop self-update (pull repo, re-run installer)
After=network-online.target
[Service]
Type=oneshot
ExecStart=/usr/bin/env bash $BASE/update.sh
EOF

  cat >/etc/systemd/system/mydesktop-update.timer <<EOF
[Unit]
Description=MyDesktop periodic self-update
[Timer]
OnBootSec=3min
OnUnitActiveSec=30min
Persistent=true
[Install]
WantedBy=timers.target
EOF

  systemctl daemon-reload
  systemctl enable --now mydesktop-update.timer
}

# ─────────────────────────── 7. nginx (ТОЛЬКО новый location) ───────────────────────────
write_nginx(){
  mkdir -p /etc/nginx/snippets
  cat >"$NGINX_SNIPPET" <<EOF
# MyDesktop — добавлено автоматически. Трогает только /mydesktop/.
location /mydesktop/ {
    proxy_pass http://127.0.0.1:$BACKEND_PORT/;
    proxy_http_version 1.1;
    proxy_set_header Host \$host;
    proxy_set_header X-Real-IP \$remote_addr;
    proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
    proxy_set_header X-Forwarded-Proto \$scheme;
    client_max_body_size 64m;
}
# HLS-плеер трансляций (проксируется на :$HLS_PORT)
location /mydesktop-hls/ {
    proxy_pass http://127.0.0.1:$HLS_PORT/;
    add_header Cache-Control no-cache;
}
EOF
  log "nginx-сниппет записан: $NGINX_SNIPPET"

  # Собственный server-блок MyDesktop на ОТДЕЛЬНОМ порту $WEB_PORT — новый файл в conf.d.
  # Чужие сайты (narelauncher и любые другие) НЕ затрагиваются ничем.
  cat >/etc/nginx/conf.d/mydesktop.conf <<EOF
# MyDesktop — собственный блок. Слушает только :$WEB_PORT, проксирует /mydesktop/ на бэкенд.
server {
    listen $WEB_PORT;
    listen [::]:$WEB_PORT;
    server_name _;
    include $NGINX_SNIPPET;
    location = / { return 302 /mydesktop/; }
}
EOF
  log "nginx: свой блок /etc/nginx/conf.d/mydesktop.conf на порту $WEB_PORT (чужие сайты не трогаются)"

  if nginx -t; then
    systemctl reload nginx
    log "nginx перезагружен — MyDesktop на :$WEB_PORT/mydesktop/"
  else
    warn "nginx -t не прошёл — conf.d/mydesktop.conf не активирован. Запустите 'nginx -t' и пришлите вывод."
  fi
}

# ─────────────────────────── финал ───────────────────────────
print_summary(){
  local pub=""
  [ -f "$RUSTDESK_DIR/id_ed25519.pub" ] && pub="$(cat "$RUSTDESK_DIR/id_ed25519.pub")"
  cat <<EOF

\033[1;32m════════ MyDesktop установлен/обновлён ════════\033[0m
  backend     :$BACKEND_PORT   -> http://127.0.0.1:$BACKEND_PORT/api/health
  веб/приложение  http://$VPS_IP:$WEB_PORT/mydesktop/   (свой nginx-блок, отдельный порт)
  RTMP push   rtmp://$VPS_IP:$RTMP_PORT/live/<key>
  HLS         http://$VPS_IP:$HLS_PORT/live/<key>/index.m3u8
  RustDesk    ID-сервер $VPS_IP:$HBBS_PORT   релей :$HBBR_PORT
  RustDesk key: ${pub:-"(появится после первого старта hbbs: $RUSTDESK_DIR/id_ed25519.pub)"}

  NareLauncher не затронут: его пути/порты/сервисы не менялись.
EOF
}

main(){
  install_packages
  sync_repo
  deploy_files
  install_rustdesk
  write_services
  write_update_timer
  write_nginx
  print_summary
}
main "$@"
