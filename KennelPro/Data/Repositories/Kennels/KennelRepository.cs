using KennelPro.Data.Database;
using KennelPro.Interfaces.Kennels;
using KennelPro.Models.Kennels;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Kennels;

public class KennelRepository : IKennelRepository
{
    private readonly AppDbContext _context;

    public KennelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Kennel>> GetAllAsync()
    {
        return await _context.Kennels.ToListAsync();
    }

    public async Task<Kennel?> GetByIdAsync(Guid id)
    {
        return await _context.Kennels.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Kennel kennel)
    {
        await _context.Kennels.AddAsync(kennel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Kennel kennel)
    {
        _context.Kennels.Update(kennel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var kennel = await GetByIdAsync(id);

        if (kennel == null)
            return;

        _context.Kennels.Remove(kennel);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Kennels.AnyAsync(x => x.Id == id);
    }
}