using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Domain.Entities;
using RealEstateAPI.Domain.Enums;
using RealEstateAPI.Domain.Interfaces;
using RealEstateAPI.Infrastructure.Data;

namespace RealEstateAPI.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly ApplicationDbContext _context;

    public PropertyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Property?> GetByIdAsync(string id)
    {
        return await _context.Properties
            .Include(p => p.Advisor)
            .FirstOrDefaultAsync(p => p.PropertyId == id);
    }

    public async Task<(IEnumerable<Property> Properties, int TotalCount)> GetAllAsync(
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
        var query = _context.Properties.Include(p => p.Advisor).AsQueryable();

        if (type.HasValue)
            query = query.Where(p => p.Type == type.Value);

        if (status.HasValue)
            query = query.Where(p => p.Status == status.Value);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        if (zone.HasValue)
            query = query.Where(p => p.Zone == zone.Value);

        if (minArea.HasValue)
            query = query.Where(p => p.Area >= minArea.Value);

        if (maxArea.HasValue)
            query = query.Where(p => p.Area <= maxArea.Value);

        if (advisorId.HasValue)
            query = query.Where(p => p.AdvisorId == advisorId.Value);

        var totalCount = await query.CountAsync();

        var properties = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (properties, totalCount);
    }

    public async Task<Property> CreateAsync(Property property)
    {
        await _context.Properties.AddAsync(property);
        await _context.SaveChangesAsync();
        return property;
    }

    public async Task<Property> UpdateAsync(Property property)
    {
        property.UpdatedAt = DateTime.UtcNow;
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
        return property;
    }

    public async Task DeleteAsync(string id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property != null)
        {
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Properties.AnyAsync(p => p.PropertyId == id);
    }
}
