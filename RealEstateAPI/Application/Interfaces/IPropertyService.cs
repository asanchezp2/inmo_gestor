using RealEstateAPI.Application.DTOs;
using RealEstateAPI.Domain.Enums;

namespace RealEstateAPI.Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyResponseDTO?> GetByIdAsync(string id);
    Task<PropertyListResponseDTO> GetAllAsync(
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
    Task<PropertyResponseDTO> CreateAsync(PropertyCreateDTO dto);
    Task<PropertyResponseDTO> UpdateAsync(string id, PropertyUpdateDTO dto);
    Task<PropertyResponseDTO> UpdateStatusAsync(string id, PropertyStatusUpdateDTO dto);
    Task DeleteAsync(string id);
}
