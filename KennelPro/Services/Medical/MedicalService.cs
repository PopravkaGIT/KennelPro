using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;

namespace KennelPro.Services.Medical;

public class MedicalService
{
    private readonly IMedicalRecordRepository _medicalRepository;

    public MedicalService(IMedicalRecordRepository medicalRepository)
    {
        _medicalRepository = medicalRepository;
    }

    public async Task<List<MedicalRecord>> GetAllAsync()
    {
        return await _medicalRepository.GetAllAsync();
    }

    public async Task<MedicalRecord?> GetByIdAsync(Guid id)
    {
        return await _medicalRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(MedicalRecord record)
    {
        await _medicalRepository.AddAsync(record);
    }

    public async Task UpdateAsync(MedicalRecord record)
    {
        await _medicalRepository.UpdateAsync(record);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _medicalRepository.DeleteAsync(id);
    }
}