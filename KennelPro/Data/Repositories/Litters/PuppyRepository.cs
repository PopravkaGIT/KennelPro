using KennelPro.Data.Database;
using KennelPro.Interfaces.Litters;
using KennelPro.Models.Litters;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Litters;

public class PuppyRepository : IPuppyRepository
{
    private readonly AppDbContext _context;

    public PuppyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Puppy>> GetAllAsync()
    {
        return await _context.Puppies.ToListAsync();
    }
    public async Task<IEnumerable<Puppy>> GetByLitterIdAsync(Guid litterId) => await _context.Puppies.Where(x => x.LitterId == litterId).OrderBy(x => x.Number).ToListAsync();

    public async Task<Puppy?> GetByIdAsync(Guid id)
    {
        return await _context.Puppies.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Puppy puppy)
    {
        await _context.Puppies.AddAsync(puppy);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Puppy puppy)
    {
        _context.Puppies.Update(puppy);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return;

        _context.Puppies.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Puppies.AnyAsync(x => x.Id == id);
    }
}
