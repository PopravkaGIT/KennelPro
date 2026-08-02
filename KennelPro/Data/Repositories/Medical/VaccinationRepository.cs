using KennelPro.Data.Database;
using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Medical;

public class VaccinationRepository : IVaccinationRepository
{
    private readonly AppDbContext _context;

    public VaccinationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Vaccination>> GetAllAsync()
    {
        return await _context.Vaccinations.ToListAsync();
    }

    public async Task<Vaccination?> GetByIdAsync(Guid id)
    {
        return await _context.Vaccinations.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Vaccination vaccination)
    {
        await _context.Vaccinations.AddAsync(vaccination);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vaccination vaccination)
    {
        _context.Vaccinations.Update(vaccination);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var vaccination = await GetByIdAsync(id);

        if (vaccination == null)
            return;

        _context.Vaccinations.Remove(vaccination);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Vaccinations.AnyAsync(x => x.Id == id);
    }
}