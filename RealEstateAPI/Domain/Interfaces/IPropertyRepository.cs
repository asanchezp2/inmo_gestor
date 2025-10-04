using RealEstateAPI.Domain.Entities;
using RealEstateAPI.Domain.Enums;

namespace RealEstateAPI.Domain.Interfaces;

public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync(string id);
    Task<(IEnumerable<Property> Properties, int TotalCount)> GetAllAsync(
        PropertyType? type,
        PropertyStatus? status,
        decimal? minPrice,
        decimal? maxPrice,
        Zone? zone,
        decimal? minArea,
        decimal? maxArea,
        int? advisorId,
        int page,
        int pageSize);
    Task<Property> CreateAsync(Property property);
    Task<Property> UpdateAsync(Property property);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
