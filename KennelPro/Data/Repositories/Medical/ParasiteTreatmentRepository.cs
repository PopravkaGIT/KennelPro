using KennelPro.Data.Database;
using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Medical;

public class ParasiteTreatmentRepository : IParasiteTreatmentRepository
{
    private readonly AppDbContext _context;

    public ParasiteTreatmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ParasiteTreatment>> GetAllAsync()
    {
        return await _context.ParasiteTreatments.ToListAsync();
    }

    public async Task<ParasiteTreatment?> GetByIdAsync(Guid id)
    {
        return await _context.ParasiteTreatments.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(ParasiteTreatment treatment)
    {
        await _context.ParasiteTreatments.AddAsync(treatment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ParasiteTreatment treatment)
    {
        _context.ParasiteTreatments.Update(treatment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return;

        _context.ParasiteTreatments.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.ParasiteTreatments.AnyAsync(x => x.Id == id);
    }
}