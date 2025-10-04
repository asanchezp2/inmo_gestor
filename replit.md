# Real Estate API - .NET 8 REST API

## Overview
A comprehensive .NET 8 REST API for real estate property management with PostgreSQL database (Neon), Entity Framework Core ORM, layered architecture, comprehensive CRUD operations, filtering capabilities, pagination support, business rule validation, and Swagger/OpenAPI documentation.

**Status**: ✅ Fully operational with PostgreSQL database
**Last Updated**: October 2, 2025

## Recent Changes
- **2025-10-02**: Successfully configured PostgreSQL (Neon) database integration
  - Converted from SQL Server to PostgreSQL using Npgsql.EntityFrameworkCore.PostgreSQL
  - Implemented DATABASE_URL parsing for Replit environment
  - Created and ran migrations successfully
  - Seeded database with 5 advisors and 20 properties
  - All API endpoints tested and working correctly

## Project Architecture

### Technology Stack
- **.NET 8 SDK**: Latest LTS version
- **PostgreSQL (Neon)**: Cloud-hosted PostgreSQL database
- **Entity Framework Core 8.0**: ORM with code-first approach
- **Npgsql.EntityFrameworkCore.PostgreSQL 8.0**: PostgreSQL provider
- **AutoMapper 13.0**: Object-to-object mapping
- **FluentValidation 11.11**: Business rule validation
- **Swagger/Swashbuckle**: API documentation (OpenAPI 3.0)

### Architecture Pattern
Layered architecture within a single project for simplicity:
- **Controllers Layer**: RESTful API endpoints
- **Application Layer**: Services, DTOs, validators, mappings
- **Domain Layer**: Entities, enums, interfaces
- **Infrastructure Layer**: DbContext, repositories, configurations, seeding

### Database Schema

#### Advisors Table
- AdvisorId (int, PK, auto-increment)
- FullName (string, required, max 100)
- Email (string, optional, max 100)
- PrimaryPhone (string, required, max 20)
- SecondaryPhone (string, optional, max 20)
- IsActive (bool, default true)
- CreatedAt (datetime)

#### Properties Table
- PropertyId (string, PK, max 50)
- PropertyCode (string, unique, max 50)
- Type (enum: Casa, Departamento, Terreno, Local, Oficina)
- Status (enum: EnVenta, EnAlquiler, Vendido, Alquilado, Inactivo)
- Title (string, required, max 100)
- Description (string, required, max 1000)
- Price (decimal, required)
- Area (decimal, required)
- Zone (enum: Norte, Sur, Este, Oeste, Centro)
- Address (string, required, max 200)
- Bedrooms (int?, optional)
- Bathrooms (int?, optional)
- ParkingSpots (int?, optional)
- HasImages (bool)
- ImageUrls (JSON string array)
- AvailableDate (datetime)
- ClosedDate (datetime?, optional)
- CreatedAt (datetime)
- UpdatedAt (datetime)
- AdvisorId (int, FK)

**Indexes**: Type, Status, Zone, Price, PropertyCode (unique), AdvisorId

## API Endpoints

### Advisors
- `POST /api/advisors` - Create new advisor
- `GET /api/advisors` - Get all advisors
- `GET /api/advisors/{id}` - Get advisor by ID
- `PUT /api/advisors/{id}` - Update advisor
- `GET /api/advisors/{id}/properties` - Get properties by advisor

### Properties
- `POST /api/properties` - Create new property
- `GET /api/properties` - Get all properties with filtering
  - Query params: propertyType, status, minPrice, maxPrice, zone, minArea, maxArea
- `GET /api/properties/{id}` - Get property by ID
- `PUT /api/properties/{id}` - Update property
- `DELETE /api/properties/{id}` - Delete property (soft delete)
- `PATCH /api/properties/{id}/status` - Update property status

## Business Rules
1. Property code is auto-generated: `{TYPE}-{ZONE}-{RANDOM}`
2. Status transitions validated (e.g., can't move from Vendido to EnVenta)
3. Price must be positive and less than 1 billion
4. Area must be positive and less than 100,000 m²
5. Advisors must be active to be assigned properties
6. FluentValidation enforces all business rules

## API Documentation
- Swagger UI available at: `/` (root path)
- OpenAPI JSON: `/swagger/v1/swagger.json`
- Interactive API testing through Swagger UI

## Database Configuration
- **Primary**: PostgreSQL (Neon cloud database)
- **Fallback**: Can be configured for SQL Server by updating appsettings.json
- **Connection**: Automatically reads from DATABASE_URL environment variable
- **Migrations**: Entity Framework Core migrations in `Infrastructure/Migrations/`

## Test Data
The database is seeded with:
- **5 Advisors**: María García, Carlos Rodríguez, Ana Martínez, Luis Hernández, Patricia Sánchez
- **20 Properties**: Mix of houses, apartments, land, and commercial properties across different zones

## Development Notes
- CORS enabled for all origins (development mode)
- Automatic migrations on startup
- Detailed logging for database operations
- Error handling with appropriate HTTP status codes
- DTO pattern for clean API contracts
- Repository pattern for data access abstraction

## Original Requirements
- ✅ .NET 8 REST API
- ✅ SQL Server database (adapted to PostgreSQL for Replit environment)
- ✅ Entity Framework Core ORM
- ✅ Layered architecture
- ✅ Docker containerization (docker-compose.yml and Dockerfile available)
- ✅ CRUD operations for properties and advisors
- ✅ Filtering (type, status, price range, zone, area)
- ✅ Pagination support
- ✅ Business rule validation
- ✅ Swagger/OpenAPI documentation
- ✅ Seeded test data

## Notes on Database Adaptation
The original specification called for SQL Server, which requires Docker and isn't available in the Replit environment. The API has been successfully adapted to use PostgreSQL (Neon), which is fully compatible with Entity Framework Core and provides all the same functionality. The docker-compose.yml and Dockerfile for SQL Server are still available in the project for deployment in environments that support Docker.
