namespace RealEstateAPI.Application.DTOs;

public class AdvisorResponseDTO
{
    public int AdvisorId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class AdvisorSummaryDTO
{
    public int AdvisorId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PrimaryPhone { get; set; } = string.Empty;
}

public class AdvisorCreateDTO
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; }
}

public class AdvisorUpdateDTO
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; }
    public bool IsActive { get; set; }
}
