using AutoMapper;
using RealEstateAPI.Application.DTOs;
using RealEstateAPI.Application.Interfaces;
using RealEstateAPI.Domain.Entities;
using RealEstateAPI.Domain.Enums;
using RealEstateAPI.Domain.Interfaces;

namespace RealEstateAPI.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IAdvisorRepository _advisorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PropertyService> _logger;

    public PropertyService(
        IPropertyRepository propertyRepository,
        IAdvisorRepository advisorRepository,
        IMapper mapper,
        ILogger<PropertyService> logger)
    {
        _propertyRepository = propertyRepository;
        _advisorRepository = advisorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PropertyResponseDTO?> GetByIdAsync(string id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        return property == null ? null : _mapper.Map<PropertyResponseDTO>(property);
    }

    public async Task<PropertyListResponseDTO> GetAllAsync(
        PropertyType? type,
        PropertyStatus? status,
        decimal? minPrice,
        decimal? maxPrice,
        Zone? zone,
        decimal? minArea,
        decimal? maxArea,
        int? advisorId,
        int page,
        int pageSize)
    {
        var (properties, totalCount) = await _propertyRepository.GetAllAsync(
            type, status, minPrice, maxPrice, zone, minArea, maxArea, advisorId, page, pageSize);

        var propertyDTOs = _mapper.Map<IEnumerable<PropertyResponseDTO>>(properties);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PropertyListResponseDTO
        {
            Properties = propertyDTOs,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages
        };
    }

    public async Task<PropertyResponseDTO> CreateAsync(PropertyCreateDTO dto)
    {
        if (!await _advisorRepository.ExistsAsync(dto.AdvisorId))
        {
            throw new ArgumentException($"Advisor with ID {dto.AdvisorId} does not exist");
        }

        var property = _mapper.Map<Property>(dto);
        
        property.PropertyCode = GeneratePropertyCode(dto.Type, dto.Zone);
        property.PropertyId = property.PropertyCode;

        if (dto.Status == PropertyStatus.Vendido || dto.Status == PropertyStatus.NoDisponible)
        {
            property.ClosedDate = DateTime.UtcNow;
        }

        var createdProperty = await _propertyRepository.CreateAsync(property);
        
        _logger.LogInformation("Property created: {PropertyCode}", createdProperty.PropertyCode);
        
        return _mapper.Map<PropertyResponseDTO>(await _propertyRepository.GetByIdAsync(createdProperty.PropertyId));
    }

    public async Task<PropertyResponseDTO> UpdateAsync(string id, PropertyUpdateDTO dto)
    {
        var existingProperty = await _propertyRepository.GetByIdAsync(id);
        
        if (existingProperty == null)
        {
            throw new KeyNotFoundException($"Property with ID {id} not found");
        }

        _mapper.Map(dto, existingProperty);

        if (dto.AvailableDate < existingProperty.CreatedAt)
        {
            throw new ArgumentException("Available date cannot be earlier than creation date");
        }

        var updatedProperty = await _propertyRepository.UpdateAsync(existingProperty);
        
        _logger.LogInformation("Property updated: {PropertyCode}", updatedProperty.PropertyCode);
        
        return _mapper.Map<PropertyResponseDTO>(await _propertyRepository.GetByIdAsync(updatedProperty.PropertyId));
    }

    public async Task<PropertyResponseDTO> UpdateStatusAsync(string id, PropertyStatusUpdateDTO dto)
    {
        var existingProperty = await _propertyRepository.GetByIdAsync(id);
        
        if (existingProperty == null)
        {
            throw new KeyNotFoundException($"Property with ID {id} not found");
        }

        if (existingProperty.Status == PropertyStatus.Vendido && dto.Status == PropertyStatus.EnVenta)
        {
            throw new InvalidOperationException("Cannot change status from Vendido to EnVenta. Create a new property instead.");
        }

        existingProperty.Status = dto.Status;

        if (dto.Status == PropertyStatus.Vendido || dto.Status == PropertyStatus.NoDisponible)
        {
            existingProperty.ClosedDate = DateTime.UtcNow;
        }
        else
        {
            existingProperty.ClosedDate = null;
        }

        var updatedProperty = await _propertyRepository.UpdateAsync(existingProperty);
        
        _logger.LogInformation("Property status updated: {PropertyCode} to {Status}", updatedProperty.PropertyCode, dto.Status);
        
        return _mapper.Map<PropertyResponseDTO>(await _propertyRepository.GetByIdAsync(updatedProperty.PropertyId));
    }

    public async Task DeleteAsync(string id)
    {
        if (!await _propertyRepository.ExistsAsync(id))
        {
            throw new KeyNotFoundException($"Property with ID {id} not found");
        }

        await _propertyRepository.DeleteAsync(id);
        
        _logger.LogInformation("Property deleted: {PropertyId}", id);
    }

    private string GeneratePropertyCode(PropertyType type, Zone zone)
    {
        var random = new Random();
        var randomNumber = random.Next(10000, 99999);
        return $"{type.ToString().ToUpper()}-{zone.ToString().ToUpper()}-{randomNumber}";
    }
}
