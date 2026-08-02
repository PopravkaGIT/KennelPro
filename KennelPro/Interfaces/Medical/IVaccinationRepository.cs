using KennelPro.Models.Medical;

namespace KennelPro.Interfaces.Medical;

public interface IVaccinationRepository
{
    Task<List<Vaccination>> GetAllAsync();

    Task<Vaccination?> GetByIdAsync(Guid id);

    Task AddAsync(Vaccination vaccination);

    Task UpdateAsync(Vaccination vaccination);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}