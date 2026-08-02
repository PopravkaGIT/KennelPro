using KennelPro.Data.Database;
using KennelPro.Interfaces.Documents;
using KennelPro.Models.Documents;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Repositories.Documents;

public class TitleRepository : ITitleRepository
{
    private readonly AppDbContext _context;

    public TitleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Title>> GetAllAsync()
    {
        return await _context.Titles.ToListAsync();
    }

    public async Task<Title?> GetByIdAsync(Guid id)
    {
        return await _context.Titles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Title title)
    {
        await _context.Titles.AddAsync(title);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Title title)
    {
        _context.Titles.Update(title);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return;

        _context.Titles.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Titles.AnyAsync(x => x.Id == id);
    }
}