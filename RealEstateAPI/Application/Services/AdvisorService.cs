using AutoMapper;
using RealEstateAPI.Application.DTOs;
using RealEstateAPI.Application.Interfaces;
using RealEstateAPI.Domain.Entities;
using RealEstateAPI.Domain.Interfaces;

namespace RealEstateAPI.Application.Services;

public class AdvisorService : IAdvisorService
{
    private readonly IAdvisorRepository _advisorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AdvisorService> _logger;

    public AdvisorService(
        IAdvisorRepository advisorRepository,
        IMapper mapper,
        ILogger<AdvisorService> logger)
    {
        _advisorRepository = advisorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AdvisorResponseDTO?> GetByIdAsync(int id)
    {
        var advisor = await _advisorRepository.GetByIdAsync(id);
        return advisor == null ? null : _mapper.Map<AdvisorResponseDTO>(advisor);
    }

    public async Task<IEnumerable<AdvisorResponseDTO>> GetAllAsync()
    {
        var advisors = await _advisorRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AdvisorResponseDTO>>(advisors);
    }

    public async Task<AdvisorResponseDTO> CreateAsync(AdvisorCreateDTO dto)
    {
        var advisor = _mapper.Map<Advisor>(dto);
        
        var createdAdvisor = await _advisorRepository.CreateAsync(advisor);
        
        _logger.LogInformation("Advisor created: {AdvisorId} - {FullName}", createdAdvisor.AdvisorId, createdAdvisor.FullName);
        
        return _mapper.Map<AdvisorResponseDTO>(createdAdvisor);
    }

    public async Task<AdvisorResponseDTO> UpdateAsync(int id, AdvisorUpdateDTO dto)
    {
        var existingAdvisor = await _advisorRepository.GetByIdAsync(id);
        
        if (existingAdvisor == null)
        {
            throw new KeyNotFoundException($"Advisor with ID {id} not found");
        }

        _mapper.Map(dto, existingAdvisor);
        
        var updatedAdvisor = await _advisorRepository.UpdateAsync(existingAdvisor);
        
        _logger.LogInformation("Advisor updated: {AdvisorId} - {FullName}", updatedAdvisor.AdvisorId, updatedAdvisor.FullName);
        
        return _mapper.Map<AdvisorResponseDTO>(updatedAdvisor);
    }

    public async Task<IEnumerable<PropertyResponseDTO>> GetPropertiesByAdvisorIdAsync(int advisorId)
    {
        if (!await _advisorRepository.ExistsAsync(advisorId))
        {
            throw new KeyNotFoundException($"Advisor with ID {advisorId} not found");
        }

        var properties = await _advisorRepository.GetPropertiesByAdvisorIdAsync(advisorId);
        return _mapper.Map<IEnumerable<PropertyResponseDTO>>(properties);
    }
}
