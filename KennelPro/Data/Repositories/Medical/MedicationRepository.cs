using KennelPro.Data.Database;
using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Medical;

public class MedicationRepository : IMedicationRepository
{
    private readonly AppDbContext _context;

    public MedicationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Medication>> GetAllAsync()
    {
        return await _context.Medications.ToListAsync();
    }

    public async Task<Medication?> GetByIdAsync(Guid id)
    {
        return await _context.Medications.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Medication medication)
    {
        await _context.Medications.AddAsync(medication);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Medication medication)
    {
        _context.Medications.Update(medication);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return;

        _context.Medications.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Medications.AnyAsync(x => x.Id == id);
    }
}