# MyDesktop

Приложение (Windows Forms, .NET Framework 4.7) + серверная часть на VPS по `/mydesktop/`.
**Полностью изолировано от NareLauncher**: свой репозиторий, свои порты, свои сервисы.

## Структура
```
MyDesktop.sln
MyDesktop/                  — WinForms-приложение (net47, Costura.Fody → один exe)
  Program.cs                — точка входа (логин → главная форма)
  Core/                     — AppConfig, ApiClient (к :2450), Session
  UI/                       — тёмная тема, FormBase, Login, AccountWizard, MainForm
  UI/Controls/              — RoundedButton, RoundedTextBox, CardPanel
  UI/Panels/                — HomePanel (+ разделы: трансляция/эффекты/удалёнка — следующие этапы)
server/
  backend/server.js         — бэкенд :2450 (scrypt-аккаунты, update.json), только stdlib Node
  rtmp/rtmp.js              — RTMP :1936 / HLS :8001 (node-media-server)
  web/index.html            — тёмная веб-страница аккаунтов (общие с приложением)
setup.sh / update.sh        — идемпотентная установка/обновление сервера из GitHub
```

## Порты (НЕ пересекаются с NareLauncher 2445/1935/8000/2444)
| Сервис | Порт |
|---|---|
| backend | 2450 |
| RTMP | 1936 |
| HLS-HTTP | 8001 |
| RustDesk hbbs | 21115, 21116(+udp) |
| RustDesk hbbr | 21117 |

## Сборка приложения (Visual Studio)
1. Открыть `MyDesktop.sln`.
2. ПКМ по решению → **Restore NuGet Packages** (подтянет Fody, Costura.Fody, Newtonsoft.Json).
3. Конфигурация **Release** → Build. На выходе — один `MyDesktop.exe` (Costura встроит все DLL).

## Установка сервера (одна команда, один раз, от root на VPS)
```bash
MYDESKTOP_OWNER=ВАШ_GITHUB \
  curl -fsSL https://raw.githubusercontent.com/ВАШ_GITHUB/mydesktop/main/setup.sh | bash
```
Повторный запуск = обновление. Таймер `mydesktop-update.timer` сам тянет репозиторий и гоняет `update.sh`.

### nginx
Скрипт кладёт сниппет `/etc/nginx/snippets/mydesktop.conf` (только `location /mydesktop/`).
Чтобы не трогать конфиг NareLauncher автоматически, в нужный `server { }` добавьте одну строку:
```nginx
include snippets/mydesktop.conf;
```
или запустите установку с `MYDESKTOP_NGINX_SITE=/путь/к/сайту` — тогда скрипт вставит `include`
сам (с бэкапом, `nginx -t` и откатом при ошибке).

## Чек-лист приёмки
- [ ] `systemctl status mydesktop mydesktop-rtmp mydesktop-hbbs mydesktop-hbbr` — все active
- [ ] `curl http://127.0.0.1:2450/api/health` → `{"ok":true,...}`
- [ ] `/mydesktop/` открывается, создаётся/удаляется аккаунт
- [ ] приложение логинится, трансляция пушит на `rtmp://151.245.217.104:1936/live/<key>`
- [ ] NareLauncher: `narenet`, `nare-rtmp` active, сайт/эфиры/боты работают как прежде
- [ ] порты 2445/1935/8000/2444 за NareLauncher не тронуты
