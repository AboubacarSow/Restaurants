# 🍽️ Restaurants API PROJECT 

A modular and scalable **ASP.NET Core 8 Web API** project built with **Clean Architecture** principles with **Vertical Slice Approch, designed to manage restaurants and their associated data (dishes, addresses, logos...).

---

![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)
![Build](https://github.com/AboubacarSow/Restaurants/actions/workflows/dotnet.yml/badge.svg)
![License](https://img.shields.io/github/license/AboubacarSow/Restaurants)
![Azure](https://img.shields.io/badge/Deployed-Azure-blue)

---

## 🎯 Motivation

This project was built as part of my journey to master backend development with ASP.NET Core using real-world practices. The goal was to apply principles like **Clean Architecture**, automated testing, **GitHub Actions CI/CD**, and **cloud deployment on Azure** — all while developing a secure, scalable, and modular API.

---

## 🏗️ Architecture Overview

This project implements the **Clean Architecture** with clearly separated layers with **Vertical Slice Approch**:

- **Domain** – Core business logic and entities
- **Application** – Use cases, commands, queries, interfaces
- **Infrastructure** – Database access (EF Core), external services
- **API** – REST controllers and dependency injection

It also follows a **Vertical Slice Architecture** to improve modularity and testability.


### 🚀 Features

- ##### 📝 Comprehensive Logging
Implemented using Serilog.AspNetCore for detailed, configurable logging.

- ##### ❌ Global Error Handling
Centralized exception management through a custom middleware ErrorHandlingMiddleware, with dedicated exception classes per entity.

- ##### 🔄 Automatic Object Mapping
Uses AutoMapper to simplify the mapping between models and DTOs.

- ##### ✅ Request Validation
Input validation handled via FluentValidation controllers clean.

- ##### ⚙️ Asynchronous Codebase
Fully asynchronous programming model for improved scalability and performance.

- ##### 📦 Pagination with Metadata
Includes metadata such as total items, page size,page number,has next, has previews and current page in responses.

- ##### 🎯 Advanced Querying
  - ###### 🔍 Filtering: Narrow down results using query parameters

  - ###### 🧠 Searching: Perform keyword-based searches

  - ###### ↕️ Sorting: Sort results by one or more fields

  
- ##### 🏠 Root Documentation Endpoint
Root-level route offering API metadata and entry points.

- ##### 🧬 API Versioning
Enables multiple versions of the API to coexist seamlessly.

- ##### 🧠 Caching Mechanism
Response caching is implemented to optimize performance and reduce server load.


- ##### 🔐 Authentication & Authorization
- 👤 **Based on ASP.NET Identity**

- 🧾 **Bearer-based tokens with Refresh Token support**

- 🛡️ **Role-based access control**

- ##### 📘 Interactive Swagger Documentation
Fully integrated with Swagger (OpenAPI) for live API testing and exploration.

- ##### 🧪 Postman Testing Collection
Includes a ready-to-use Postman collection for easy testing of all endpoints.

- ##### 📁 File Upload & Download
Supports uploading files( restaurant's logo).

- ##### 🧪 Automated Testing Suite
    - ✅ Unit Tests using xUnit and Moq
    - 🔁 Integration Tests to validate real interactions
    - 🧪 Structured using the AAA (Arrange–Act–Assert) pattern

- ##### 🔄 CI/CD Pipeline GitHub Actions, providing
    - 🔍 Automatic building and testing and validation on every push
    - 🚀 Deployment pipeline to Azure App Services



### ☁️ Azure Services Used:
This project leverages the following Azure services for hosting, monitoring, storage, and persistence:

- **Azure App Service**	: Hosts the ASP.NET Core API in a scalable and managed environment
- **Azure Application Insights** :Provides performance monitoring, usage analytics, and centralized logging (Serilog sink)
- **Azure Blob Storage**: Stores uploaded assets such as restaurant logos
- **Azure SQL Database** Serves as the primary relational database with full persistence support



### 🛠️ Technologies Used
- 🧱 **ASP.NET Core Web API**
-  **Entity Framework Core**
- 🛢️ **MSSQL Server** for local development
- 🗃️ **Azure Data Studio**
- 📁 **Microsoft Azure Storage Explorer**
- 🔄 **AutoMapper**
- 📘 **Swagger**
- 🧪 **Postman** (for testing)
- 🧠 **Marvin.Cache.Headers**



### 🌐 Deployment Environments

- 🔧 **Development**: [View Swagger UI](https://restaurants-api-development-ewa8a2hxcvaggxau.polandcentral-01.azurewebsites.net/swagger/index.html)
- 🚀 **Production**: [View Swagger UI](https://restaurants-api-prod-abe7atcef8era0d9.polandcentral-01.azurewebsites.net/swagger/index.html)

---

## 🧪 Testing Strategy

- **Unit Testing**: Core logic tested using `xUnit` and `Moq`
- **Integration Testing**: Full request/response cycles tested through real infrastructure setup - Test for RestaurantsController
- **CI Testing**: Automatically triggered by GitHub Actions on every push by creating a pull request


## 🛠️ Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or Azure SQL instance
- (Optional) Azure Storage connection string

### Run the API Locally

```bash
git clone https://github.com/AboubacarSow/Restaurants
cd src
dotnet run --project Restaurants.API
```
## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for details.
