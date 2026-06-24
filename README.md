# Banking Loan API

Banking Loan API is an ASP.NET Core Web API for managing users, admins, loan applications, loan approval workflows, repayments, refresh-token based authentication, and loan notification emails.

The solution is organized with a layered architecture:

- `LoanAPI` - API entry point, controllers, middleware, Swagger, authentication, rate limiting, and logging.
- `Application` - DTOs, MediatR commands and queries, services, validation, mapping, and application interfaces.
- `Infrastructure` - Entity Framework Core persistence, repositories, migrations, and notification providers.
- `Core` - domain entities, enums, and loan business rules.

## Features

- User registration and management.
- Admin and accountant management.
- JWT authentication with refresh-token rotation and revoke support.
- Role-based authorization for admin and accountant actions.
- Loan creation, update, approval, rejection, and deletion.
- Loan financial calculation using configured loan rules.
- Loan repayment tracking with remaining balance snapshots.
- Loan status history.
- Email notification support through Resend.
- FluentValidation pipeline for request validation.
- SQL Server persistence with Entity Framework Core migrations.
- Swagger UI in development.
- Serilog console and rolling file logging.
- Fixed-window rate limiting for login and selected API routes.
- Docker build support.

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core with SQL Server
- MediatR
- FluentValidation
- Mapster
- JWT Bearer authentication
- Serilog
- Resend email provider
- Docker

## Project Structure

```text
BankingLoanAPI/
  Application/
    DTO/
    Features/
    Interfaces/
    Notifications/
    Services/
    Validations/
  Core/
    Entities/
    Enums/
  Infrastructure/
    DependencyInjection/
    Migrations/
    Notifications/
    Persistance/
  LoanAPI/
    Controllers/
    Middlewares/
    Services/
    Templates/
    Program.cs
  DockerFile
  LoanAPI.sln
```

## Prerequisites

- .NET 8 SDK
- SQL Server or SQL Server Express
- Entity Framework Core CLI tools
- Optional: Docker

Install EF tools if needed:

```powershell
dotnet tool install --global dotnet-ef
```

## Configuration

`LoanAPI/appsettings.json` is currently empty, so provide configuration through user secrets, environment variables, or local appsettings overrides.

Required configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BankingLoanDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Issuer": "BankingLoanAPI",
    "Audience": "BankingLoanAPI",
    "Key": "replace-with-a-long-secure-secret-key"
  },
  "Resend": {
    "ApiKey": "replace-with-resend-api-key"
  },
  "Notifications": {
    "Email": {
      "FromAddress": "no-reply@example.com"
    }
  },
   "Serilog": {
   "MinimumLevel": {
     "Default": "Information",
     "Override": {
       "Microsoft": "Warning",
       "Microsoft.EntityFrameworkCore": "Warning",
       "System": "Warning"
     }
   }
 }
}
```

For local development, prefer user secrets:

```powershell
cd LoanAPI
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=BankingLoanDb;Trusted_Connection=True;TrustServerCertificate=True;"
dotnet user-secrets set "Jwt:Issuer" "BankingLoanAPI"
dotnet user-secrets set "Jwt:Audience" "BankingLoanAPI"
dotnet user-secrets set "Jwt:Key" "replace-with-a-long-secure-secret-key"
dotnet user-secrets set "Resend:ApiKey" "replace-with-resend-api-key"
dotnet user-secrets set "Notifications:Email:FromAddress" "no-reply@example.com"
```

Do not commit real connection strings, JWT secrets, or API keys.

## Database Setup

Apply migrations:

```powershell
dotnet ef database update -p Infrastructure -s LoanAPI
```

Create a new migration:

```powershell
dotnet ef migrations add MigrationName -p Infrastructure -s LoanAPI
```

## Running Locally

Restore and build:

```powershell
dotnet restore
dotnet build
```

Run the API:

```powershell
dotnet run --project LoanAPI
```

Development URLs from `launchSettings.json`:

- `https://localhost:7026`
- `http://localhost:5026`

Swagger is available in development at:

```text
https://localhost:7026/swagger
```

## Docker

Build the image:

```powershell
docker build -f DockerFile -t banking-loan-api .
```

Run the container:

```powershell
docker run -p 8080:8080 banking-loan-api
```

Pass production configuration through environment variables or a secret manager.

## Authentication

The API uses JWT access tokens and refresh tokens.

Login:

```http
POST /api/Auth/login
```

Request:

```json
{
  "username": "user1",
  "password": "password"
}
```

Response:

```json
{
  "token": "jwt-access-token",
  "expiration": "2026-06-23T12:00:00Z",
  "refreshToken": "refresh-token"
}
```

Refresh access token:

```http
POST /api/Auth/refresh
```

Request:

```json
{
  "refreshToken": "refresh-token"
}
```

Revoke refresh token:

```http
POST /api/Auth/revoke
```

Use the JWT access token in protected routes:

```http
Authorization: Bearer {token}
```

## Main API Endpoints

### Auth

| Method | Route | Description |
| --- | --- | --- |
| POST | `/api/Auth/login` | Login and issue access and refresh tokens |
| POST | `/api/Auth/refresh` | Rotate refresh token and issue a new access token |
| POST | `/api/Auth/revoke` | Revoke a refresh token |

### Users

| Method | Route | Description |
| --- | --- | --- |
| POST | `/api/User/register` | Register a user |
| GET | `/api/User/{id}` | Get user by id |
| GET | `/api/User` | Get users, admin/accountant only |
| DELETE | `/api/User/{id}` | Delete user |
| PUT | `/api/User/block/{id}` | Block user, accountant only |

### Admins

| Method | Route | Description |
| --- | --- | --- |
| POST | `/api/Admin/create` | Create an admin user |
| POST | `/api/Admin/add` | Add accountant/admin user, admin only |
| GET | `/api/Admin` | Get all admins, admin only |
| GET | `/api/Admin/{id}` | Get admin by id, admin only |
| PUT | `/api/Admin` | Update admin user, admin only |
| DELETE | `/api/Admin/{id}` | Delete admin user, admin only |

### Loans

| Method | Route | Description |
| --- | --- | --- |
| POST | `/api/Loan/create` | Create a loan request |
| GET | `/api/Loan/{id}` | Get loan by id |
| GET | `/api/Loan` | Get paged loans, admin/accountant only |
| PUT | `/api/Loan/update/{id}` | Update loan |
| DELETE | `/api/Loan/{id}` | Delete loan |
| POST | `/api/Loan/approve/{loanId}` | Approve loan, accountant only |
| POST | `/api/Loan/reject/{loanId}` | Reject loan, accountant only |

### Repayments

| Method | Route | Description |
| --- | --- | --- |
| POST | `/api/loans/{loanId}/repayments` | Record a loan repayment |
| GET | `/api/loans/{loanId}/repayments` | Get repayment summary and history |

## Loan Repayments

Repayments track loan debt, not wallet or account balance.

Example:

```text
Loan TotalRepayment: 500

Payment 1: 100
  RemainingBalanceBefore: 500
  RemainingBalanceAfter: 400

Payment 2: 200
  RemainingBalanceBefore: 400
  RemainingBalanceAfter: 200

Payment 3: 200
  RemainingBalanceBefore: 200
  RemainingBalanceAfter: 0
  Loan status: Completed
```

The accountant records that the borrower paid money outside the system. The API stores repayment history and updates the loan status when the debt is fully paid.

## Roles

Supported admin roles:

- `Admin`
- `Accountant`

Supported user types:

- `Individual`
- `Entrepreneur`
- `Company`

Supported loan types:

- `QuickLoan`
- `CarLoan`
- `Installment`

Supported loan statuses:

- `InProcess`
- `Approved`
- `Rejected`
- `Completed`

## Logging

The API uses Serilog and writes logs to:

```text
LoanAPI/Logs/log-.txt
```

Logs roll daily and keep the latest 7 files.

## Rate Limiting

Configured policies:

- `login`: 5 requests per minute.
- `api`: 60 requests per minute.

The API returns HTTP `429 Too Many Requests` when a policy is exceeded.

## Notes for Contributors

- Keep domain entities in `Core`.
- Keep request handling in `Application/Features`.
- Keep database code in `Infrastructure/Persistance`.
- Keep controllers thin and route requests through MediatR.
- Store secrets outside source control.
- Add EF migrations when changing persisted entities.
