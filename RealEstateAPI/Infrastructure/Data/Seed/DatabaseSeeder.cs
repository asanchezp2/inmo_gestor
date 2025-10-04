using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Domain.Entities;
using RealEstateAPI.Domain.Enums;

namespace RealEstateAPI.Infrastructure.Data.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Advisors.AnyAsync())
        {
            return;
        }

        var advisors = new List<Advisor>
        {
            new Advisor
            {
                FullName = "María García González",
                Email = "maria.garcia@realestate.com",
                PrimaryPhone = "+52 55 1234 5678",
                SecondaryPhone = "+52 55 8765 4321",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-6)
            },
            new Advisor
            {
                FullName = "Carlos Rodríguez Pérez",
                Email = "carlos.rodriguez@realestate.com",
                PrimaryPhone = "+52 55 2345 6789",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-5)
            },
            new Advisor
            {
                FullName = "Ana Martínez López",
                Email = "ana.martinez@realestate.com",
                PrimaryPhone = "+52 55 3456 7890",
                SecondaryPhone = "+52 55 9876 5432",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-4)
            },
            new Advisor
            {
                FullName = "Luis Hernández Silva",
                PrimaryPhone = "+52 55 4567 8901",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-3)
            },
            new Advisor
            {
                FullName = "Patricia Sánchez Torres",
                Email = "patricia.sanchez@realestate.com",
                PrimaryPhone = "+52 55 5678 9012",
                SecondaryPhone = "+52 55 8765 1234",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddMonths(-2)
            }
        };

        await context.Advisors.AddRangeAsync(advisors);
        await context.SaveChangesAsync();

        var random = new Random(12345);
        var properties = new List<Property>
        {
            new Property
            {
                PropertyId = "CASA-NORTE-47382",
                PropertyCode = "CASA-NORTE-47382",
                Type = PropertyType.Casa,
                Status = PropertyStatus.EnVenta,
                Title = "Casa moderna en zona residencial",
                Description = "Hermosa casa de 3 recámaras con acabados de lujo, jardín amplio y cochera para 2 autos. Ubicada en zona tranquila y segura.",
                Price = 3500000,
                Area = 250,
                Zone = Zone.Norte,
                Address = "Av. Pino Suárez 245, Col. Residencial Norte",
                Bedrooms = 3,
                Bathrooms = 2,
                ParkingSpots = 2,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Casa+Moderna\", \"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Interior\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                UpdatedAt = DateTime.UtcNow.AddDays(-30),
                AdvisorId = 1
            },
            new Property
            {
                PropertyId = "DEPTO-CENTRO-89234",
                PropertyCode = "DEPTO-CENTRO-89234",
                Type = PropertyType.Departamento,
                Status = PropertyStatus.EnAlquiler,
                Title = "Departamento céntrico con vista panorámica",
                Description = "Departamento amueblado de 2 recámaras en piso 12, con vista a la ciudad. Incluye amenidades completas.",
                Price = 15000,
                Area = 95,
                Zone = Zone.Centro,
                Address = "Calle Reforma 890, Centro Histórico",
                Bedrooms = 2,
                Bathrooms = 1,
                ParkingSpots = 1,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Departamento+Centro\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-25),
                CreatedAt = DateTime.UtcNow.AddDays(-25),
                UpdatedAt = DateTime.UtcNow.AddDays(-25),
                AdvisorId = 2
            },
            new Property
            {
                PropertyId = "TERRENO-SUR-56789",
                PropertyCode = "TERRENO-SUR-56789",
                Type = PropertyType.Terreno,
                Status = PropertyStatus.EnVenta,
                Title = "Terreno comercial sobre avenida principal",
                Description = "Terreno plano ideal para desarrollo comercial o residencial. Todos los servicios disponibles.",
                Price = 2800000,
                Area = 500,
                Zone = Zone.Sur,
                Address = "Av. Insurgentes Sur Km 8.5",
                HasImages = false,
                ImageUrls = "[]",
                AvailableDate = DateTime.UtcNow.AddDays(-20),
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                UpdatedAt = DateTime.UtcNow.AddDays(-20),
                AdvisorId = 1
            },
            new Property
            {
                PropertyId = "LOCAL-ESTE-12345",
                PropertyCode = "LOCAL-ESTE-12345",
                Type = PropertyType.LocalComercial,
                Status = PropertyStatus.EnAlquiler,
                Title = "Local comercial en plaza establecida",
                Description = "Local comercial de 80m² en plaza con alto tráfico peatonal y vehicular. Baño y bodega incluidos.",
                Price = 22000,
                Area = 80,
                Zone = Zone.Este,
                Address = "Plaza del Sol, Local 15-A",
                Bathrooms = 1,
                ParkingSpots = 2,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Local+Comercial\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-15),
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow.AddDays(-15),
                AdvisorId = 3
            },
            new Property
            {
                PropertyId = "OFICINA-OESTE-67890",
                PropertyCode = "OFICINA-OESTE-67890",
                Type = PropertyType.Oficina,
                Status = PropertyStatus.EnAnticredito,
                Title = "Oficina ejecutiva en torre corporativa",
                Description = "Oficina amueblada con 3 privados, sala de juntas, kitchenette y baños. Incluye 3 cajones de estacionamiento.",
                Price = 4200000,
                Area = 120,
                Zone = Zone.Oeste,
                Address = "Torre Empresarial Poniente, Piso 8",
                Bathrooms = 2,
                ParkingSpots = 3,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Oficina+Ejecutiva\", \"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Sala+Juntas\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow.AddDays(-10),
                AdvisorId = 4
            },
            new Property
            {
                PropertyId = "CASA-CENTRO-23456",
                PropertyCode = "CASA-CENTRO-23456",
                Type = PropertyType.Casa,
                Status = PropertyStatus.Vendido,
                Title = "Casa colonial restaurada",
                Description = "Casa histórica completamente restaurada con detalles originales. 4 recámaras, patio central y terraza.",
                Price = 5800000,
                Area = 320,
                Zone = Zone.Centro,
                Address = "Calle Hidalgo 123, Centro",
                Bedrooms = 4,
                Bathrooms = 3,
                ParkingSpots = 2,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Casa+Colonial\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-45),
                ClosedDate = DateTime.UtcNow.AddDays(-5),
                CreatedAt = DateTime.UtcNow.AddDays(-45),
                UpdatedAt = DateTime.UtcNow.AddDays(-5),
                AdvisorId = 5
            },
            new Property
            {
                PropertyId = "DEPTO-NORTE-78901",
                PropertyCode = "DEPTO-NORTE-78901",
                Type = PropertyType.Departamento,
                Status = PropertyStatus.EnVenta,
                Title = "Departamento nuevo con amenidades",
                Description = "Departamento de estreno con 3 recámaras, amenidades completas: alberca, gym, salón de eventos.",
                Price = 2950000,
                Area = 110,
                Zone = Zone.Norte,
                Address = "Residencial Bosques del Norte, Torre B 402",
                Bedrooms = 3,
                Bathrooms = 2,
                ParkingSpots = 2,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Depto+Nuevo\", \"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Amenidades\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-8),
                CreatedAt = DateTime.UtcNow.AddDays(-8),
                UpdatedAt = DateTime.UtcNow.AddDays(-8),
                AdvisorId = 2
            },
            new Property
            {
                PropertyId = "TERRENO-ESTE-34567",
                PropertyCode = "TERRENO-ESTE-34567",
                Type = PropertyType.Terreno,
                Status = PropertyStatus.EnVenta,
                Title = "Terreno residencial con proyecto aprobado",
                Description = "Terreno con proyecto arquitectónico aprobado para 6 townhouses. Excelente ubicación residencial.",
                Price = 3200000,
                Area = 600,
                Zone = Zone.Este,
                Address = "Privada Los Cedros s/n",
                HasImages = false,
                ImageUrls = "[]",
                AvailableDate = DateTime.UtcNow.AddDays(-12),
                CreatedAt = DateTime.UtcNow.AddDays(-12),
                UpdatedAt = DateTime.UtcNow.AddDays(-12),
                AdvisorId = 1
            },
            new Property
            {
                PropertyId = "LOCAL-SUR-45678",
                PropertyCode = "LOCAL-SUR-45678",
                Type = PropertyType.LocalComercial,
                Status = PropertyStatus.EnVenta,
                Title = "Local esquinero con gran afluencia",
                Description = "Local comercial en esquina con doble acceso, ideal para restaurante o retail. Instalaciones completas.",
                Price = 4500000,
                Area = 150,
                Zone = Zone.Sur,
                Address = "Av. Universidad esquina con Morelos",
                Bathrooms = 2,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Local+Esquinero\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-18),
                CreatedAt = DateTime.UtcNow.AddDays(-18),
                UpdatedAt = DateTime.UtcNow.AddDays(-18),
                AdvisorId = 3
            },
            new Property
            {
                PropertyId = "CASA-SUR-56780",
                PropertyCode = "CASA-SUR-56780",
                Type = PropertyType.Casa,
                Status = PropertyStatus.EnAlquiler,
                Title = "Casa amueblada en privada",
                Description = "Casa completamente amueblada y equipada en privada con seguridad 24/7. Lista para habitar.",
                Price = 28000,
                Area = 180,
                Zone = Zone.Sur,
                Address = "Privada Las Palmas 45",
                Bedrooms = 3,
                Bathrooms = 2,
                ParkingSpots = 2,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Casa+Amueblada\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-6),
                CreatedAt = DateTime.UtcNow.AddDays(-6),
                UpdatedAt = DateTime.UtcNow.AddDays(-6),
                AdvisorId = 4
            },
            new Property
            {
                PropertyId = "OFICINA-CENTRO-67891",
                PropertyCode = "OFICINA-CENTRO-67891",
                Type = PropertyType.Oficina,
                Status = PropertyStatus.NoDisponible,
                Title = "Oficina en remodelación",
                Description = "Oficina en proceso de remodelación. Estará disponible próximamente con acabados de primera.",
                Price = 35000,
                Area = 90,
                Zone = Zone.Centro,
                Address = "Edificio Central, Piso 5",
                Bathrooms = 1,
                ParkingSpots = 2,
                HasImages = false,
                ImageUrls = "[]",
                AvailableDate = DateTime.UtcNow.AddDays(30),
                ClosedDate = DateTime.UtcNow.AddDays(-2),
                CreatedAt = DateTime.UtcNow.AddDays(-22),
                UpdatedAt = DateTime.UtcNow.AddDays(-2),
                AdvisorId = 5
            },
            new Property
            {
                PropertyId = "DEPTO-OESTE-89012",
                PropertyCode = "DEPTO-OESTE-89012",
                Type = PropertyType.Departamento,
                Status = PropertyStatus.EnVenta,
                Title = "Penthouse con terraza privada",
                Description = "Exclusivo penthouse de 2 niveles con terraza de 100m², jacuzzi y vista 360°. Acabados de lujo.",
                Price = 8900000,
                Area = 280,
                Zone = Zone.Oeste,
                Address = "Residencial Altavista, Penthouse 1",
                Bedrooms = 4,
                Bathrooms = 4,
                ParkingSpots = 3,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Penthouse\", \"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Terraza\", \"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Vista\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-14),
                CreatedAt = DateTime.UtcNow.AddDays(-14),
                UpdatedAt = DateTime.UtcNow.AddDays(-14),
                AdvisorId = 1
            },
            new Property
            {
                PropertyId = "TERRENO-NORTE-90123",
                PropertyCode = "TERRENO-NORTE-90123",
                Type = PropertyType.Terreno,
                Status = PropertyStatus.EnAnticredito,
                Title = "Terreno industrial con bodega",
                Description = "Terreno con bodega de 200m² construida, ideal para uso industrial o logístico. Fácil acceso.",
                Price = 6500000,
                Area = 1000,
                Zone = Zone.Norte,
                Address = "Parque Industrial Norte, Lote 12",
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Terreno+Industrial\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-28),
                CreatedAt = DateTime.UtcNow.AddDays(-28),
                UpdatedAt = DateTime.UtcNow.AddDays(-28),
                AdvisorId = 2
            },
            new Property
            {
                PropertyId = "CASA-ESTE-01234",
                PropertyCode = "CASA-ESTE-01234",
                Type = PropertyType.Casa,
                Status = PropertyStatus.EnVenta,
                Title = "Casa minimalista con alberca",
                Description = "Diseño minimalista moderno, 3 recámaras con baño, alberca, jardín y pérgola. Smart home.",
                Price = 6200000,
                Area = 300,
                Zone = Zone.Este,
                Address = "Fraccionamiento Los Robles 789",
                Bedrooms = 3,
                Bathrooms = 3,
                ParkingSpots = 2,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Casa+Minimalista\", \"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Alberca\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-11),
                CreatedAt = DateTime.UtcNow.AddDays(-11),
                UpdatedAt = DateTime.UtcNow.AddDays(-11),
                AdvisorId = 3
            },
            new Property
            {
                PropertyId = "LOCAL-NORTE-12346",
                PropertyCode = "LOCAL-NORTE-12346",
                Type = PropertyType.LocalComercial,
                Status = PropertyStatus.EnAlquiler,
                Title = "Local en centro comercial premium",
                Description = "Local comercial en centro comercial de alto nivel. Excelente ubicación, alto tráfico de clientes.",
                Price = 45000,
                Area = 120,
                Zone = Zone.Norte,
                Address = "Centro Comercial Plaza Norte, Local 201",
                Bathrooms = 1,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Local+Premium\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-9),
                CreatedAt = DateTime.UtcNow.AddDays(-9),
                UpdatedAt = DateTime.UtcNow.AddDays(-9),
                AdvisorId = 4
            },
            new Property
            {
                PropertyId = "OFICINA-SUR-23457",
                PropertyCode = "OFICINA-SUR-23457",
                Type = PropertyType.Oficina,
                Status = PropertyStatus.EnVenta,
                Title = "Oficina con bodega en parque empresarial",
                Description = "Oficina con área de bodega integrada, ideal para empresas de distribución. Seguridad 24/7.",
                Price = 3800000,
                Area = 200,
                Zone = Zone.Sur,
                Address = "Parque Empresarial Sur, Módulo 15",
                Bathrooms = 2,
                ParkingSpots = 4,
                HasImages = false,
                ImageUrls = "[]",
                AvailableDate = DateTime.UtcNow.AddDays(-16),
                CreatedAt = DateTime.UtcNow.AddDays(-16),
                UpdatedAt = DateTime.UtcNow.AddDays(-16),
                AdvisorId = 5
            },
            new Property
            {
                PropertyId = "DEPTO-SUR-34568",
                PropertyCode = "DEPTO-SUR-34568",
                Type = PropertyType.Departamento,
                Status = PropertyStatus.EnAlquiler,
                Title = "Departamento amueblado para ejecutivos",
                Description = "Departamento totalmente amueblado, servicios incluidos, ideal para ejecutivos. Estancia corta o larga.",
                Price = 18000,
                Area = 75,
                Zone = Zone.Sur,
                Address = "Torre Ejecutiva Sur 305",
                Bedrooms = 1,
                Bathrooms = 1,
                ParkingSpots = 1,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Depto+Ejecutivo\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-4),
                CreatedAt = DateTime.UtcNow.AddDays(-4),
                UpdatedAt = DateTime.UtcNow.AddDays(-4),
                AdvisorId = 1
            },
            new Property
            {
                PropertyId = "CASA-OESTE-45679",
                PropertyCode = "CASA-OESTE-45679",
                Type = PropertyType.Casa,
                Status = PropertyStatus.EnVenta,
                Title = "Casa campestre con terreno amplio",
                Description = "Casa estilo campestre en terreno de 800m², jardín, área de asador, pozo de agua propio.",
                Price = 4800000,
                Area = 350,
                Zone = Zone.Oeste,
                Address = "Camino Real a la Sierra Km 5",
                Bedrooms = 4,
                Bathrooms = 3,
                ParkingSpots = 3,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Casa+Campestre\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-21),
                CreatedAt = DateTime.UtcNow.AddDays(-21),
                UpdatedAt = DateTime.UtcNow.AddDays(-21),
                AdvisorId = 2
            },
            new Property
            {
                PropertyId = "TERRENO-CENTRO-56781",
                PropertyCode = "TERRENO-CENTRO-56781",
                Type = PropertyType.Terreno,
                Status = PropertyStatus.EnVenta,
                Title = "Terreno urbano céntrico con plusvalía",
                Description = "Terreno plano en zona de alta plusvalía, ideal para desarrollo vertical. Uso de suelo mixto.",
                Price = 12000000,
                Area = 450,
                Zone = Zone.Centro,
                Address = "Calle Juárez 567",
                HasImages = false,
                ImageUrls = "[]",
                AvailableDate = DateTime.UtcNow.AddDays(-35),
                CreatedAt = DateTime.UtcNow.AddDays(-35),
                UpdatedAt = DateTime.UtcNow.AddDays(-35),
                AdvisorId = 3
            },
            new Property
            {
                PropertyId = "LOCAL-OESTE-67892",
                PropertyCode = "LOCAL-OESTE-67892",
                Type = PropertyType.LocalComercial,
                Status = PropertyStatus.EnVenta,
                Title = "Local comercial con estacionamiento propio",
                Description = "Local con 10 cajones de estacionamiento exclusivos. Zona de alto crecimiento comercial.",
                Price = 5500000,
                Area = 180,
                Zone = Zone.Oeste,
                Address = "Blvd. Poniente 1234",
                Bathrooms = 2,
                ParkingSpots = 10,
                HasImages = true,
                ImageUrls = "[\"https://placehold.co/800x600/0066CC/FFFFFF/png?text=Local+con+Estacionamiento\"]",
                AvailableDate = DateTime.UtcNow.AddDays(-13),
                CreatedAt = DateTime.UtcNow.AddDays(-13),
                UpdatedAt = DateTime.UtcNow.AddDays(-13),
                AdvisorId = 4
            }
        };

        await context.Properties.AddRangeAsync(properties);
        await context.SaveChangesAsync();
    }
}
