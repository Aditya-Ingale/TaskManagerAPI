# TaskManagerAPI

A simple and powerful **Task Management REST API** built with **ASP.NET Core** and **PostgreSQL**.  
Manage your tasks and users with full **CRUD operations**, proper **validation**, and **error handling**.

---

## Features

- ✨ Create, Read, Update, and Delete (CRUD) operations for **Tasks** and **Users**
- 🔐 Secure database credentials using a `.env` file
- 🛡️ Model Validation with Custom Error Responses
- 🔄 PostgreSQL Database Integration using **Entity Framework Core**
- 📖 API Documentation with **Swagger UI**
- 🖇️ Proper Folder Structure (Controllers, DTOs, Models, Data)
- 🔄 Circular Reference Handling in JSON responses

---

## Tech Stack

- **Framework:** ASP.NET Core (.NET 8)
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core
- **API Documentation:** Swagger / OpenAPI
- **Environment Variables Management:** dotenv.net

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Aditya-Ingale/TaskManagerAPI.git
cd TaskManagerAPI
```

### 2. Set up the Environment Variables
- Create a .env file in the root of the project:
```bash
touch .env
```
- Fill it with your PostgreSQL credentials:
```bash
DB_HOST=localhost
DB_PORT=5432
DB_NAME=your_database_name
DB_USER=your_username
DB_PASSWORD=your_password
```

### 3. Apply Migrations (if needed)
```bash
dotnet ef database update
```
- (Or let EF auto-create the database when you run.)

### 4. Run the API
```bash
dotnet run
```

- Visit:
- https://localhost:5001/swagger — to explore your API with Swagger UI!

### API Endpoints Overview
HTTP Method | Endpoint | Description
GET | /api/tasks | Get all tasks
GET | /api/tasks/{id} | Get a specific task
POST | /api/tasks | Create a new task
PUT | /api/tasks/{id} | Update a task
DELETE | /api/tasks/{id} | Delete a task
... | ... | (More endpoints for Users)

### Folder structure
```bash
TaskManagerAPI/
├── Controllers/
├── DTO/
├── Models/
├── Data/
├── Properties/
├── .env
├── appsettings.json
├── Program.cs
├── README.md
└── .gitignore
```

### License
This project is licensed under the MIT License — feel free to use it for your own projects!
