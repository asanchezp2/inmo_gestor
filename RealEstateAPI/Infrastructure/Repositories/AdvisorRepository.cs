using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Domain.Entities;
using RealEstateAPI.Domain.Interfaces;
using RealEstateAPI.Infrastructure.Data;

namespace RealEstateAPI.Infrastructure.Repositories;

public class AdvisorRepository : IAdvisorRepository
{
    private readonly ApplicationDbContext _context;

    public AdvisorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Advisor?> GetByIdAsync(int id)
    {
        return await _context.Advisors
            .Include(a => a.Properties)
            .FirstOrDefaultAsync(a => a.AdvisorId == id);
    }

    public async Task<IEnumerable<Advisor>> GetAllAsync()
    {
        return await _context.Advisors
            .OrderBy(a => a.FullName)
            .ToListAsync();
    }

    public async Task<Advisor> CreateAsync(Advisor advisor)
    {
        await _context.Advisors.AddAsync(advisor);
        await _context.SaveChangesAsync();
        return advisor;
    }

    public async Task<Advisor> UpdateAsync(Advisor advisor)
    {
        _context.Advisors.Update(advisor);
        await _context.SaveChangesAsync();
        return advisor;
    }

    public async Task<IEnumerable<Property>> GetPropertiesByAdvisorIdAsync(int advisorId)
    {
        return await _context.Properties
            .Where(p => p.AdvisorId == advisorId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Advisors.AnyAsync(a => a.AdvisorId == id);
    }
}
