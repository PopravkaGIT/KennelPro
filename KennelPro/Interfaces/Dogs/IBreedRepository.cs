using KennelPro.Models.Dogs;

namespace KennelPro.Interfaces.Dogs;

public interface IBreedRepository
{
    Task<List<Breed>> GetAllAsync();

    Task<Breed?> GetByIdAsync(Guid id);

    Task AddAsync(Breed breed);

    Task UpdateAsync(Breed breed);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}