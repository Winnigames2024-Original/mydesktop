#!/usr/bin/env bash
# =============================================================================
#  MyDesktop — обновление по таймеру (mydesktop-update.timer вызывает этот файл).
#  Делает git pull репозитория и повторно гоняет идемпотентный setup.sh.
#  NareLauncher не трогает. Повторный запуск безопасен.
# =============================================================================
set -euo pipefail

BASE=/opt/mydesktop
REPO_DIR="$BASE/repo"
[ -f "$BASE/mydesktop.env" ] && . "$BASE/mydesktop.env" || true

log(){ printf '\033[1;36m[mydesktop-update]\033[0m %s\n' "$*"; }

[ "$(id -u)" = "0" ] || { echo "update.sh: нужен root"; exit 1; }

if [ ! -d "$REPO_DIR/.git" ]; then
  log "Репозиторий ещё не склонирован — запускаю установку."
fi

# setup.sh сам делает fetch+reset до origin/<branch>, установку пакетов,
# деплой и перезапуск сервисов — всё идемпотентно.
if [ -f "$REPO_DIR/setup.sh" ]; then
  log "Обновляю через репозиторный setup.sh…"
  exec /usr/bin/env bash "$REPO_DIR/setup.sh"
else
  OWNER="${MYDESKTOP_OWNER:-CHANGE_ME}"
  BRANCH="${MYDESKTOP_BRANCH:-main}"
  REPO="${MYDESKTOP_REPO:-mydesktop}"
  log "Нет локального setup.sh — тяну с GitHub…"
  exec /usr/bin/env bash -c "curl -fsSL https://raw.githubusercontent.com/${OWNER}/${REPO}/${BRANCH}/setup.sh | bash"
fi
