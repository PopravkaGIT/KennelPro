using KennelPro.Data.Database;
using KennelPro.Interfaces.Reproduction;
using KennelPro.Models.Reproduction;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Reproduction;

public class MatingRepository : IMatingRepository
{
    private readonly AppDbContext _context;

    public MatingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Mating>> GetAllAsync()
    {
        return await _context.Matings.ToListAsync();
    }

    public async Task<Mating?> GetByIdAsync(Guid id)
    {
        return await _context.Matings.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Mating mating)
    {
        await _context.Matings.AddAsync(mating);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Mating mating)
    {
        _context.Matings.Update(mating);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return;

        _context.Matings.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Matings.AnyAsync(x => x.Id == id);
    }
}