using RealEstateAPI.Application.DTOs;

namespace RealEstateAPI.Application.Interfaces;

public interface IAdvisorService
{
    Task<AdvisorResponseDTO?> GetByIdAsync(int id);
    Task<IEnumerable<AdvisorResponseDTO>> GetAllAsync();
    Task<AdvisorResponseDTO> CreateAsync(AdvisorCreateDTO dto);
    Task<AdvisorResponseDTO> UpdateAsync(int id, AdvisorUpdateDTO dto);
    Task<IEnumerable<PropertyResponseDTO>> GetPropertiesByAdvisorIdAsync(int advisorId);
}
