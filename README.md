# ASP.NET Core Web API Project

## Overview

This project is built using **ASP.NET Core (.NET 9)** and provides a scalable, production-ready foundation for building RESTful APIs. It integrates modern tools for authentication, data access, logging, cloud storage, and API documentation.

---

## Tech Stack

* **Framework:** .NET 9 (ASP.NET Core Web API)
* **ORM:** Entity Framework Core + Dapper
* **Authentication:** JWT Bearer Tokens
* **Logging:** Serilog (File sink)
* **Cloud Storage:** AWS S3
* **Email Service:** MailKit
* **Object Mapping:** AutoMapper
* **API Documentation:** Swagger (Swashbuckle) + Scalar UI

---

## Features

* JWT-based authentication & authorization
* Hybrid data access (EF Core + Dapper)
* AWS S3 integration for file storage
* Email sending capability
* Structured logging with Serilog
* API documentation with Swagger & Scalar
* Query filtering support
* Secure password hashing with BCrypt

---

## Project Structure

```id="a1b2c3"
/Interface              # Interfaces and contracts
/Properties
  /PublishProfiles      # Deployment profiles
```

Recommended extensions:

* `Controllers/`
* `Services/`
* `Repositories/`
* `Models/`
* `DTOs/`
* `Middlewares/`

---

## Package Version Policy

This project uses **strict, fixed versions** for all NuGet packages.
All versions listed below are **mandatory** and must not be changed without proper review and testing.

### Why this matters

* Ensures consistent builds across environments
* Prevents breaking changes from dependency updates
* Maintains runtime stability
* Avoids unexpected production issues

### Rules

* Do NOT upgrade/downgrade packages arbitrarily
* All changes must go through testing and code review
* Validate compatibility with .NET 9 before updating

---

## Locked Package Versions

| Package                                             | Version |
| --------------------------------------------------- | ------- |
| AutoMapper.Extensions.Microsoft.DependencyInjection | 12.0.1  |
| AWSSDK.S3                                           | 3.7.300 |
| BCrypt.Net-Next                                     | 4.0.3   |
| Dapper                                              | 2.1.72  |
| Dekiru.QueryFilter                                  | 9.5.3   |
| MailKit                                             | 4.13.0  |
| Microsoft.AspNetCore.Authentication.JwtBearer       | 9.0.8   |
| Microsoft.AspNetCore.OpenApi                        | 9.0.8   |
| Microsoft.EntityFrameworkCore                       | 9.0.8   |
| Microsoft.EntityFrameworkCore.Design                | 9.0.8   |
| Microsoft.EntityFrameworkCore.SqlServer             | 9.0.8   |
| Microsoft.EntityFrameworkCore.Tools                 | 9.0.8   |
| Newtonsoft.Json                                     | 13.0.3  |
| Scalar.AspNetCore                                   | 2.6.9   |
| Serilog.AspNetCore                                  | 9.0.0   |
| Serilog.Sinks.File                                  | 7.0.0   |
| Swashbuckle.AspNetCore                              | 9.0.3   |
| System.IdentityModel.Tokens.Jwt                     | 8.14.0  |

---

## Getting Started

### Prerequisites

* .NET 9 SDK
* SQL Server
* AWS account (for S3)

---

### Installation


### Configuration

Update `appsettings.json`:

* Database connection string
* JWT configuration
* AWS credentials
* SMTP/email settings

---

## Database Setup

```bash id="db1"
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Running the Application

```bash id="run1"
dotnet run
```

Default URL:

---

## Authentication

Uses JWT Bearer tokens:

Flow:

1. User logs in
2. Server returns JWT
3. Client includes token in requests

---

## Logging

Implemented using Serilog:

* Writes logs to file
* Configurable via `appsettings.json`
* Supports structured logging

---

## API Documentation

* Swagger UI: `/swagger`
* Scalar UI: `/scalar`

---

## Deployment


## Updating Packages (Strict Process)

1. Create a feature branch
2. Update only required package
3. Perform full testing:

   * Authentication (JWT)
   * Database operations
   * External services (AWS, Email)
4. Submit pull request with:

   * Justification
   * Changelog summary
   * Risk assessment

---


## Security Notes

* Passwords are hashed using BCrypt
* Store secrets securely (environment variables or secret managers)
* Always use HTTPS in production

---

## Future Improvements

* Unit and integration testing
* Redis caching
* Role-based authorization
* Docker support
* CI/CD pipelines

---
