using KennelPro.Data.Database;
using KennelPro.Interfaces.Dogs;
using KennelPro.Models.Dogs;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Dogs;

public class BreedRepository : IBreedRepository
{
    private readonly AppDbContext _context;

    public BreedRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Breed>> GetAllAsync()
    {
        return await _context.Breeds.ToListAsync();
    }

    public async Task<Breed?> GetByIdAsync(Guid id)
    {
        return await _context.Breeds.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Breed breed)
    {
        await _context.Breeds.AddAsync(breed);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Breed breed)
    {
        _context.Breeds.Update(breed);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var breed = await GetByIdAsync(id);

        if (breed == null)
            return;

        _context.Breeds.Remove(breed);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Breeds.AnyAsync(x => x.Id == id);
    }
}