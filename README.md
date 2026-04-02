# Finance Dashboard Backend

## 📌 Overview
This project is a backend system for a Finance Dashboard that allows users to manage financial records and view summary analytics based on roles.

Built using ASP.NET Core Web API and SQL Server.

---

## 🚀 Features

### 👥 User Management
- Create and manage users
- Role-based access control (Admin, Analyst, Viewer)
- Active/Inactive user support

### 💰 Financial Records
- Create, update, delete financial records
- Filter by type, category, and date
- Tracks income and expenses

### 📊 Dashboard Analytics
- Total Income
- Total Expense
- Net Balance
- Category-wise breakdown
- Recent transactions

---

## 🔐 Role-Based Access

| Role    | Permissions |
|---------|------------|
| Admin   | Full access |
| Analyst | View + dashboard |
| Viewer  | Read-only |

---

## 🛠️ Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server (LocalDB)
- Swagger (API testing)

---

## ⚙️ Setup Instructions

1. Clone repository
2. Update connection string in `appsettings.json`
3. Run:

dotnet ef database update

4. Run project:

dotnet run

5. Open Swagger:

https://localhost
:<port>/swagger


---

## 📡 API Usage

### Headers (for role simulation)

x-user-role: Admin


---

## 📌 Sample Endpoints

### Users
- `POST /api/users`
- `GET /api/users`

### Financial Records
- `POST /api/records`
- `GET /api/records?type=Expense`

### Dashboard
- `GET /api/dashboard/summary`

---

## 🧠 Design Decisions

- Used DTOs to avoid exposing sensitive data
- Implemented role-based access using request headers
- Used EF Core for database operations and migrations
- Designed dashboard APIs for aggregated analytics

---

## 🚀 Future Improvements

- JWT Authentication
- Pagination
- Unit Testing
- Role-based middleware
- Data validation enhancements

---

## 👨‍💻 Author
Kulbhushan
