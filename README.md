# Конфигурация

## more-tech-backend

### Зависимости

Сервис зависит от следующих внешних сервисов:

1. newscatcher https://newscatcher.p.rapidapi.com

### Подключение к БД

| Environment variable          | Description                                                     | Default value |
|-------------------------------|-----------------------------------------------------------------|---------------|
| ConnectionString              | Подключение к БД MSSQL                                          |               |

### Пути до внешних сервисов сервисов

| Environment variable                | Description             | Default value |
|-------------------------------------|-------------------------|---------------|
| REST_ZIF_OM_NEWSCATCHER_URL         | URL до сервиса новостей |               |

### Прочие настройки

| Environment variable | Description                                  | Default value |
|----------------------|----------------------------------------------|---------------|
| NEWSCATCHER_API_KEY  | Ключ авторизации во внешнем сервисе новостей |               |

### Как развернуть

1. запустить docker-compose файл, для этого перейти в корень проекта и выолнить команду docker-compose up
2. для наполнения базы заготовленными данными необходимо скачать бекап по ссылке https://drive.google.com/file/d/1NyXhpDXPeI2junsySbEdtbqccHyB6ej-/view?usp=sharing 
2.1. загрузить этот бекап в контейнер с БД
2.2. выполнить рестор базы с добавленным бекапом
3. если ресторить базу желания нет, то надо дернуть апи PUT administration/migrations для накатывания миграций
приложение готово к использованию по адресу http://localhost:5129/swagger/index.html
``
