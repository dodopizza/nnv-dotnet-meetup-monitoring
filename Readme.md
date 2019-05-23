# nnv-meetup-monitoring

## Что это?
Код c лайвкодинг сессии с нижегородского .net митапа 22 мая 2019 года.

Тестовый проект на .net с настроенным экспортом метрик, скрипты для запуска **prometheus** и **grafana**, готовые дэшборды для графаны.

## Локальный запуск
Мы намеренно отказались от сборки в docker, чтобы упростить понимание происходящего:
- `./build.sh` собирает .net проект
- `./run.sh` запускает .net проект, графану и prometheus в отдельных котенерах в рамках одной сети, чтобы можно было посмотреть локально
- `./load.py` - нагружает API, чтобы можно было посмотреть на графики

При первом запуске, в графане нужно добавить источник данных вручную (http://prometheus:9090). 

В графану можно импортировать два готовых дэшборда из папки `grafana`: 
- `tech_dashboard.json`
- `business_dashboard.json`

## Запуск на проде
- `./prod_prometheus.sh`
- `./prod_grafana.sh`

Важно: скрипты предназначены для linux машин, у Mac нет поддержки host-сети

Disclaimer: Это минимальная конфигурация для запуска. Хардкодить пароли в скриптах – плохая практика, также как запускать один инстанс системы, на которую собираешься полагаться. Prometheus не предлагает встроенных стредств аутентификации и авторизации, не выставляйте его в большой интернет. Have fun. 