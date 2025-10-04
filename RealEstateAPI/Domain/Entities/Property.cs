using RealEstateAPI.Domain.Enums;

namespace RealEstateAPI.Domain.Entities;

public class Property
{
    public string PropertyId { get; set; } = string.Empty;
    public string PropertyCode { get; set; } = string.Empty;
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
    public bool HasImages { get; set; }
    public string ImageUrls { get; set; } = "[]";
    public DateTime AvailableDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public int AdvisorId { get; set; }
    
    public Advisor? Advisor { get; set; }
}
