
# Alza Products Web API
This repository contains a .NET Web API application for retrieving and modifying sample product data.


## Technologies Used
* .NET 10
* ASP.NET Core
* Entity Framework Core
* SQL Server Express LocalDB
* Swagger (Swashbuckle)
* xUnit and Moq for testing


## Architecture
The solution follows clean architecture and it is separated into 4 layers:

*  **`Alza.Products.Domain`**: The core of the application. It contains the domain entities, such as the `Product` class.

*  **`Alza.Products.Application`**: Contains the application logic. It defines interfaces (`IProductRepository`, `IProductService`) and implements services (`ProductService`) that orchestrate the domain objects to perform business operations. It also includes Data Transfer Objects (DTOs) for communicating with the presentation layer.

*  **`Alza.Products.Infrastructure`**: Implements the data access logic defined in the Application layer. It uses Entity Framework Core to interact with the database (`ProductDbContext`, `ProductRepository`). It also handles database seeding.

*  **`Alza.Products.WebApi`**: The presentation layer. This is an ASP.NET Core Web API that exposes the application's functionality via REST API endpoints. It includes controllers, API versioning, Swagger documentation, and global exception handling.


## Database Initialization & Seeding

This application supports optional database creation/deletion and data seeding controlled via `appsettings.json`:
```bash
"Database": {
	"EnsureDeleted": false,
    "EnsureCreated": false,
    "SeedData": false
  }
```
* `EnsureDeleted`:  When enabled, the database gets deleted on startup, if it exists (as the first step in this pipeline).
* `EnsureCreated`: When enabled, the application ensures the database schema is created on startup, if it doesn't already exist. (as the second step).
* `SeedData`: When enabled, initial sample data is inserted into the database on startup (as the third step). The data will only get populated if EnsureCreated is also set to true.


## Getting Started

### Prerequisites
* .NET SDK 
* SQL Server Express LocalDB

### 1.  **Clone the repository:**
```bash
git clone https://github.com/andrea-k/alza.products.git
cd alza.products
```

### 2.  **Create and populate the database**
Inside the `Alza.Products.WebApi` project, open`appsettings.json` file and change the `Database.EnsureCreated` and `Database.EnsureCreated` settings to `true`order to create the `AlzaProducts` database and populate the `Products` table with seed data:
```bash
"Database": {
	"EnsureDeleted": false,
    "EnsureCreated": true,
    "SeedData": true
  }
```
Optionally, you can also modify `ConnectionStrings.DefaultConnection`:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AlzaProducts;Trusted_Connection=True"
},
```
(You can find a list of SQL local DB instances on your machine with this termial command):
```bash
sqllocaldb i
```

### 2.  **Run the application:**
You can run the application using the .NET CLI or by opening the solution in Visual Studio and starting the `Alza.Products.WebApi` project.
```bash
dotnet run --project Alza.Products.WebApi
```

### 3.  **Access the API:**
Once the application is running, it will automatically create and seed the `AlzaProducts` database and you can now access the API at http://localhost:5106


## API Endpoints
The following endpoints are available. Detailed documentation is available at `https://localhost:5106/swagger`.

*  **GET**  `/api/v1/products`
*  **GET**  `/api/v2/products`
*  **GET**  `/api/v{version}/products/{id}`
*  **PATCH**  `/api/v{version}/products/{id}/description`


## Testing
Unit test projects for each layer of the application are placed in the `tests` folder. To run the tests, use the Visual Studio Test Explorer or execute the following command from the root directory:
```bash
dotnet  test
```