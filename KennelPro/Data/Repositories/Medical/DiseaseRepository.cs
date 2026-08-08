using KennelPro.Data.Database;
using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Medical;

public class DiseaseRepository : IDiseaseRepository
{
    private readonly AppDbContext _context;

    public DiseaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Disease>> GetAllAsync()
    {
        return await _context.Diseases.ToListAsync();
    }

    public async Task<IEnumerable<Disease>> GetByDogIdAsync(Guid dogId)
    {
        return await _context.Diseases
            .Where(x => x.DogId == dogId)
            .OrderByDescending(x => x.StartDate)
            .ToListAsync();
    }

    public async Task<Disease?> GetByIdAsync(Guid id)
    {
        return await _context.Diseases.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Disease disease)
    {
        await _context.Diseases.AddAsync(disease);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Disease disease)
    {
        _context.Diseases.Update(disease);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return;

        _context.Diseases.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Diseases.AnyAsync(x => x.Id == id);
    }
}
