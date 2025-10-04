using RealEstateAPI.Domain.Enums;

namespace RealEstateAPI.Application.DTOs;

public class PropertyResponseDTO
{
    public string PropertyId { get; set; } = string.Empty;
    public string PropertyCode { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Area { get; set; }
    public string Zone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public int? ParkingSpots { get; set; }
    public bool HasImages { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public DateTime AvailableDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int AdvisorId { get; set; }
    public AdvisorSummaryDTO? Advisor { get; set; }
}

public class PropertyCreateDTO
{
    public PropertyType Type { get; set; }
    public PropertyStatus Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Area { get; set; }
    public Zone Zone { get; set; }
    public string Address { get; set; } = string.Empty;
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public int? ParkingSpots { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public DateTime AvailableDate { get; set; }
    public int AdvisorId { get; set; }
}

public class PropertyUpdateDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Area { get; set; }
    public string Address { get; set; } = string.Empty;
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public int? ParkingSpots { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public DateTime AvailableDate { get; set; }
}

public class PropertyStatusUpdateDTO
{
    public PropertyStatus Status { get; set; }
}

public class PropertyListResponseDTO
{
    public IEnumerable<PropertyResponseDTO> Properties { get; set; } = new List<PropertyResponseDTO>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
