# 📞 TelephoneConversations

**TelephoneConversations** – це веб-застосунок для обліку телефонних переговорів юридичних осіб із можливістю формування відомостей про використані послуги.

## 🚀 Функціонал:
- Робота з абонентами, дзвінками, тарифами та знижками.
- Формування відомостей про використані послуги за абонентами та містами.
- Гнучке управління тарифами та знижками.
- **Фронтенд:** HTML, CSS, JavaScript.
- **Бекенд:** ASP.NET Core Web API, Entity Framework Core.
- **База даних:** SQL Server.

---

## 🛠️ Встановлення та запуск

### 📌 1. **Попередні вимоги**
Перед тим, як запустити проєкт, вам потрібно:
- **[Visual Studio 2022+](https://visualstudio.microsoft.com/downloads/)**
- **[.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
- **[SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### 📌 2. Клонування проєкту

Склонуйте репозиторій за допомогою Git:

git clone https://github.com/Vlad-Bilyk/TelephoneConversations.git

### 📌 3. Налаштування бази даних
3.1. Створіть базу даних в SQL Server

Відкрийте SQL Server Management Studio або будь-який інший клієнт баз даних та створіть базу даних.
```
CREATE DATABASE TelConvAPI;
```
3.2. Налаштуйте appsettings.json

Знайдіть файл appsettings.json у TelephoneConversations.API і змініть рядок підключення:
```
{
  "ConnectionStrings": {
    "DefaultSQLConnection": "Server=YOUR_SERVER_NAME;Database=TelConvAPI;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
### 📌 4. Застосування міграцій

Перейдіть у консоль Visual Studio (Tools → Nuget Package Manager → Package Manager Console) оберіть TelephoneConversations.DataAccess як Default project та виконайте команду:

```update-database```

### 📌 5. Запуск проєкту

Запустіть проєкт у Visual Studio (через https)

🌍 Тепер API працює за адресою:

https://localhost:7119/

#### Проект запускається на сторінці авторизації
```
Login: admin

Пароль: 12345
```
#### Також ви можете переглянути API-endpoint за адресою: https://localhost:7119/swagger/index.html
