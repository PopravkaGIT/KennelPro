using KennelPro.Data.Database;
using KennelPro.Interfaces.Reproduction;
using KennelPro.Models.Reproduction;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Reproduction;

public class HeatCycleRepository : IHeatCycleRepository
{
    private readonly AppDbContext _context;

    public HeatCycleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<HeatCycle>> GetAllAsync()
    {
        return await _context.HeatCycles.ToListAsync();
    }

    public async Task<HeatCycle?> GetByIdAsync(Guid id)
    {
        return await _context.HeatCycles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(HeatCycle heatCycle)
    {
        await _context.HeatCycles.AddAsync(heatCycle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(HeatCycle heatCycle)
    {
        _context.HeatCycles.Update(heatCycle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return;

        _context.HeatCycles.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.HeatCycles.AnyAsync(x => x.Id == id);
    }
}