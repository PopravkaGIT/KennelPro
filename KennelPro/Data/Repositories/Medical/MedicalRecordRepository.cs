using KennelPro.Data.Database;
using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Medical;

public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly AppDbContext _context;

    public MedicalRecordRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MedicalRecord>> GetAllAsync()
    {
        return await _context.MedicalRecords.ToListAsync();
    }

    public async Task<IEnumerable<MedicalRecord>> GetByDogIdAsync(Guid dogId)
    {
        return await _context.MedicalRecords
            .Where(x => x.DogId == dogId)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
    }

    public async Task<MedicalRecord?> GetByIdAsync(Guid id)
    {
        return await _context.MedicalRecords.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(MedicalRecord medicalRecord)
    {
        await _context.MedicalRecords.AddAsync(medicalRecord);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MedicalRecord medicalRecord)
    {
        _context.MedicalRecords.Update(medicalRecord);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var record = await GetByIdAsync(id);

        if (record == null)
            return;

        _context.MedicalRecords.Remove(record);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.MedicalRecords.AnyAsync(x => x.Id == id);
    }
}
