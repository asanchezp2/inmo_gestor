namespace RealEstateAPI.Domain.Entities;

public class Advisor
{
    public int AdvisorId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Property> Properties { get; set; } = new List<Property>();
}
