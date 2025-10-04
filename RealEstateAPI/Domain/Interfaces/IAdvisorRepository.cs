using RealEstateAPI.Domain.Entities;

namespace RealEstateAPI.Domain.Interfaces;

public interface IAdvisorRepository
{
    Task<Advisor?> GetByIdAsync(int id);
    Task<IEnumerable<Advisor>> GetAllAsync();
    Task<Advisor> CreateAsync(Advisor advisor);
    Task<Advisor> UpdateAsync(Advisor advisor);
    Task<IEnumerable<Property>> GetPropertiesByAdvisorIdAsync(int advisorId);
    Task<bool> ExistsAsync(int id);
}
