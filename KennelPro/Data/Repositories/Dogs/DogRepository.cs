using KennelPro.Data.Database;
using KennelPro.Interfaces.Dogs;
using KennelPro.Models.Dogs;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Dogs;

public class DogRepository : IDogRepository
{
    private readonly AppDbContext _context;

    public DogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Dog>> GetAllAsync()
    {
        return await _context.Dogs
            .Include(d => d.Breed)
            .Include(d => d.Kennel)
            .ToListAsync();
    }

    public async Task<List<Dog>> GetByKennelIdAsync(Guid kennelId)
    {
        return await _context.Dogs
            .Include(d => d.Breed)
            .Include(d => d.Kennel)
            .Where(d => d.KennelId == kennelId)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<Dog?> GetByIdAsync(Guid id)
    {
        return await _context.Dogs
            .Include(d => d.Breed)
            .Include(d => d.Kennel)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddAsync(Dog dog)
    {
        await _context.Dogs.AddAsync(dog);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Dog dog)
    {
        _context.Dogs.Update(dog);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var dog = await GetByIdAsync(id);

        if (dog == null)
            return;

        _context.Dogs.Remove(dog);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Dogs.AnyAsync(d => d.Id == id);
    }
}