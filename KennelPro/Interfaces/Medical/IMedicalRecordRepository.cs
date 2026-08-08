using KennelPro.Models.Medical;

namespace KennelPro.Interfaces.Medical;

public interface IMedicalRecordRepository
{
    Task<List<MedicalRecord>> GetAllAsync();

    Task<IEnumerable<MedicalRecord>> GetByDogIdAsync(Guid dogId);

    Task<MedicalRecord?> GetByIdAsync(Guid id);

    Task AddAsync(MedicalRecord medicalRecord);

    Task UpdateAsync(MedicalRecord medicalRecord);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}
