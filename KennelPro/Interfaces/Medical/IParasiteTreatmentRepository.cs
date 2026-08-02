using KennelPro.Models.Medical;

namespace KennelPro.Interfaces.Medical;

public interface IParasiteTreatmentRepository
{
    Task<List<ParasiteTreatment>> GetAllAsync();

    Task<ParasiteTreatment?> GetByIdAsync(Guid id);

    Task AddAsync(ParasiteTreatment treatment);

    Task UpdateAsync(ParasiteTreatment treatment);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}