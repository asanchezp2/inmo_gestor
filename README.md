# Real Estate API

A comprehensive .NET 8 REST API for real estate property management with PostgreSQL database, Entity Framework Core ORM, and complete CRUD operations.

## 🚀 Features

- ✅ **Full CRUD Operations** for Properties and Advisors
- ✅ **Advanced Filtering** by type, status, price range, zone, and area
- ✅ **PostgreSQL Database** with Entity Framework Core
- ✅ **Layered Architecture** for maintainability
- ✅ **Business Rule Validation** with FluentValidation
- ✅ **AutoMapper** for clean DTO mapping
- ✅ **Swagger/OpenAPI Documentation** at root path
- ✅ **Seeded Test Data** (5 advisors, 20 properties)

## 🛠️ Technology Stack

- .NET 8 SDK
- PostgreSQL (Neon cloud database)
- Entity Framework Core 8.0
- Npgsql.EntityFrameworkCore.PostgreSQL 8.0
- AutoMapper 13.0
- FluentValidation 11.11
- Swagger/Swashbuckle

## 📋 API Endpoints

### Advisors
```
POST   /api/advisors              Create new advisor
GET    /api/advisors              Get all advisors
GET    /api/advisors/{id}         Get advisor by ID
PUT    /api/advisors/{id}         Update advisor
GET    /api/advisors/{id}/properties  Get properties by advisor
```

### Properties
```
POST   /api/properties            Create new property
GET    /api/properties            Get all properties (with filters)
GET    /api/properties/{id}       Get property by ID
PUT    /api/properties/{id}       Update property
DELETE /api/properties/{id}       Delete property
PATCH  /api/properties/{id}/status Update property status
```

### Query Parameters for GET /api/properties
- `propertyType` - Filter by property type (Casa, Departamento, Terreno, etc.)
- `status` - Filter by status (EnVenta, EnAlquiler, Vendido, etc.)
- `minPrice` / `maxPrice` - Filter by price range
- `zone` - Filter by zone (Norte, Sur, Este, Oeste, Centro)
- `minArea` / `maxArea` - Filter by area range

## 🗄️ Database Schema

### Advisors
- AdvisorId (int, auto-increment)
- FullName (required)
- Email (optional)
- PrimaryPhone (required)
- SecondaryPhone (optional)
- IsActive (default true)
- CreatedAt

### Properties
- PropertyId (string, primary key)
- PropertyCode (unique)
- Type (enum: Casa, Departamento, Terreno, Local, Oficina)
- Status (enum: EnVenta, EnAlquiler, Vendido, Alquilado, Inactivo)
- Title, Description
- Price, Area
- Zone (enum: Norte, Sur, Este, Oeste, Centro)
- Address
- Bedrooms, Bathrooms, ParkingSpots (optional)
- Images support
- AvailableDate, ClosedDate
- CreatedAt, UpdatedAt
- AdvisorId (foreign key)

## 🚦 Running the API

### Prerequisites
- .NET 8 SDK
- PostgreSQL database (or use environment DATABASE_URL)
- Docker

### Run
```bash
docker-compose -f .\docker-compose.yml up --build
```

The API will be available at `http://localhost:5000`

### API Documentation
Open your browser and navigate to:
- **Swagger UI**: `http://localhost:5000/`
- **OpenAPI JSON**: `http://localhost:5000/swagger/v1/swagger.json`

## 📊 Test Data

The database is automatically seeded with:
- **5 Advisors**: María García, Carlos Rodríguez, Ana Martínez, Luis Hernández, Patricia Sánchez
- **20 Properties**: Various types including houses, apartments, land, and commercial properties

## 🔒 Business Rules

1. Property codes are auto-generated: `{TYPE}-{ZONE}-{RANDOM}`
2. Status transitions are validated
3. Price must be positive and < 1,000,000,000
4. Area must be positive and < 100,000 m²
5. Only active advisors can be assigned to properties

## 🏗️ Architecture

The project follows a layered architecture pattern:

```
RealEstateAPI/
├── Controllers/          # API endpoints
├── Application/
│   ├── Services/        # Business logic
│   ├── DTOs/            # Data transfer objects
│   ├── Validators/      # FluentValidation rules
│   └── Mappings/        # AutoMapper profiles
├── Domain/
│   ├── Entities/        # Domain models
│   ├── Enums/           # Enumerations
│   └── Interfaces/      # Repository contracts
└── Infrastructure/
    ├── Data/            # DbContext & configurations
    ├── Repositories/    # Data access implementation
    └── Migrations/      # EF Core migrations
```

## 🔧 Configuration

### Environment Variables
- `DATABASE_URL` - PostgreSQL connection string (auto-configured in Replit)

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RealEstateDB;..."
  }
}
```

## 🐳 Docker Support

Docker configuration files are included:
- `Dockerfile` - Multi-stage build for .NET 8 API
- `docker-compose.yml` - SQL Server configuration (for non-Replit environments)

## 📝 Example Requests

### Get All Properties
```bash
curl http://localhost:5000/api/properties
```

### Filter Properties
```bash
curl "http://localhost:5000/api/properties?status=EnVenta&minPrice=2000000&maxPrice=5000000"
```

### Get Property by ID
```bash
curl http://localhost:5000/api/properties/CASA-NORTE-47382
```

### Create New Advisor
```bash
curl -X POST http://localhost:5000/api/advisors \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Juan Pérez",
    "email": "juan@example.com",
    "primaryPhone": "+52 55 1111 2222"
  }'
```

## 📄 License

This project is for demonstration purposes (PSU-IA-DEV-ACT01).

## 👨‍💻 Author

Armando Sánchez Pérez
