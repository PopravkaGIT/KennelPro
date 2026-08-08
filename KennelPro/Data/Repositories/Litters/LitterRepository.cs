using KennelPro.Data.Database;
using KennelPro.Interfaces.Litters;
using KennelPro.Models.Litters;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Litters;

public class LitterRepository : ILitterRepository
{
    private readonly AppDbContext _context;

    public LitterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Litter>> GetAllAsync()
    {
        return await _context.Litters.ToListAsync();
    }
    public async Task<IEnumerable<Litter>> GetByKennelIdAsync(Guid kennelId) => await _context.Litters.Include(x => x.MotherDog).Include(x => x.FatherDog).Where(x => x.MotherDog.KennelId == kennelId && x.FatherDog.KennelId == kennelId).OrderByDescending(x => x.BirthDate).ToListAsync();

    public async Task<Litter?> GetByIdAsync(Guid id)
    {
        return await _context.Litters.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Litter litter)
    {
        await _context.Litters.AddAsync(litter);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Litter litter)
    {
        _context.Litters.Update(litter);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return;

        _context.Litters.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Litters.AnyAsync(x => x.Id == id);
    }
}
