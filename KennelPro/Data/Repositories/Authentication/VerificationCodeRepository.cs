using KennelPro.Data.Database;
using KennelPro.Interfaces.Authentication;
using KennelPro.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Authentication;

public class VerificationCodeRepository : IVerificationCodeRepository
{
    private readonly AppDbContext _context;

    public VerificationCodeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<VerificationCode>> GetAllAsync()
    {
        return await _context.VerificationCodes.ToListAsync();
    }

    public async Task<VerificationCode?> GetByIdAsync(Guid id)
    {
        return await _context.VerificationCodes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<VerificationCode?> GetByCodeAsync(string code)
    {
        return await _context.VerificationCodes.FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task AddAsync(VerificationCode code)
    {
        await _context.VerificationCodes.AddAsync(code);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(VerificationCode code)
    {
        _context.VerificationCodes.Update(code);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return;

        _context.VerificationCodes.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.VerificationCodes.AnyAsync(x => x.Id == id);
    }
}