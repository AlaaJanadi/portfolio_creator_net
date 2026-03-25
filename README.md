# Portfolio Creator API

A RESTful backend API built with **ASP.NET Core (.NET 8)** that lets users register, authenticate with JWT, and build digital portfolios containing personal information and education history.

## Features

- **JWT Authentication** – Register, login, refresh tokens, and logout
- **Portfolio Management** – Create and update professional summaries, toggle public/private visibility
- **Personal Information** – Store contact details and social media links (GitHub, LinkedIn, Twitter, Instagram, Facebook)
- **Education History** – Add, update, and delete education entries with dates, descriptions, and certificate URLs
- **Swagger / Scalar API Docs** – Interactive API documentation available in development

## Tech Stack

| Layer | Technology |
|---|---|
| Runtime | .NET 8 / ASP.NET Core |
| Authentication | JWT Bearer (`Microsoft.AspNetCore.Authentication.JwtBearer`) |
| Database | SQL Server via Entity Framework Core 8 |
| API Docs | Swagger (OpenAPI) + Scalar |

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server running on `localhost,1433`

### Configuration

Update `JWTAuth/appsettings.json` with your settings:

```json
{
  "AppSettings": {
    "Token": "<your-256-bit-secret-key>",
    "Issuer": "AlaaApp",
    "Audience": "AlaaAppAudience"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=jwt_net8;User Id=sa;Password=<your-password>;TrustServerCertificate=True"
  }
}
```

### Run the API

```bash
# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update --project JWTAuth

# Start the development server
dotnet run --project JWTAuth/JWTAuth.csproj
```

The API will be available at `http://localhost:5272`.  
Interactive API docs (Scalar) will be available at `http://localhost:5272/scalar/v1`.

## API Endpoints

### Auth

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Login and receive JWT tokens |
| POST | `/api/auth/refresh-token` | Refresh an expired access token |
| POST | `/api/auth/logout/{userId}` | Logout (invalidates refresh token) |

### Portfolio

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/portfolio/{portfolioId}` | Get portfolio details |
| POST | `/api/portfolio/{portfolioId}/professional-summary` | Update professional summary |
| PUT | `/api/portfolio/{portfolioId}/is-public` | Toggle public/private visibility |

### Personal Information

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/portfolio/PersonalInfo/{portfolioId}` | Get personal information |
| POST | `/api/portfolio/PersonalInfo/{portfolioId}` | Create or update personal information |

### Education

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/portfolio/Education/{portfolioId}` | Get all education entries |
| GET | `/api/portfolio/Education/{portfolioId}/{educationId}` | Get a single education entry |
| POST | `/api/portfolio/Education/{portfolioId}` | Add an education entry |
| PUT | `/api/portfolio/Education/{portfolioId}/{educationId}` | Update an education entry |
| DELETE | `/api/portfolio/Education/{portfolioId}/{educationId}` | Delete an education entry |

## Project Structure

```
JWTAuth/
├── Controllers/     # API endpoints (Auth, Portfolio, PersonalInfo, Education)
├── Services/        # Business logic (interfaces + implementations)
├── Entities/        # EF Core entity models
├── Dtos/            # Data Transfer Objects
├── Mapper/          # Entity ↔ DTO mapping helpers
├── Jwt/             # JWT token generation and validation
├── Data/            # EF Core DbContext and migrations
├── Program.cs       # App configuration and DI setup
└── appsettings.json # Application configuration
```

## License

This project is open source. Feel free to use and modify it.
