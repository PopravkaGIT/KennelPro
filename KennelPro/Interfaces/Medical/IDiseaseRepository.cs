using KennelPro.Models.Medical;

namespace KennelPro.Interfaces.Medical;

public interface IDiseaseRepository
{
    Task<List<Disease>> GetAllAsync();

    Task<Disease?> GetByIdAsync(Guid id);

    Task AddAsync(Disease disease);

    Task UpdateAsync(Disease disease);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}