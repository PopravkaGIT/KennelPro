using KennelPro.Models.Medical;

namespace KennelPro.Interfaces.Medical;

public interface IMedicationRepository
{
    Task<List<Medication>> GetAllAsync();

    Task<Medication?> GetByIdAsync(Guid id);

    Task AddAsync(Medication medication);

    Task UpdateAsync(Medication medication);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}