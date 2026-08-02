using KennelPro.Models.Kennels;

namespace KennelPro.Interfaces.Kennels;

public interface IKennelRepository
{
    Task<List<Kennel>> GetAllAsync();

    Task<Kennel?> GetByIdAsync(Guid id);

    Task AddAsync(Kennel kennel);

    Task UpdateAsync(Kennel kennel);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}