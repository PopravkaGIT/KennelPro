using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using KennelPro.Services.Dogs;
using KennelPro.Validators;

namespace KennelPro.Services.Medical;

public class MedicalService
{
    private readonly IMedicalRecordRepository _medicalRepository;
    private readonly DogService _dogService;

    public MedicalService(
        IMedicalRecordRepository medicalRepository,
        DogService dogService)
    {
        _medicalRepository = medicalRepository;
        _dogService = dogService;
    }

    public async Task<List<MedicalRecord>> GetForDogAsync(Guid dogId)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(dogId) == null)
            return new List<MedicalRecord>();

        return (await _medicalRepository.GetByDogIdAsync(dogId)).ToList();
    }

    public async Task<MedicalRecord?> GetByIdAsync(Guid id)
    {
        var record = await _medicalRepository.GetByIdAsync(id);
        if (record == null)
            return null;

        if (await _dogService.GetDogForCurrentKennelAsync(record.DogId) == null)
            return null;

        return record;
    }

    public async Task<(bool Success, string Error)> AddAsync(MedicalRecord record)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(record.DogId) == null)
            return (false, "Dog not found.");

        record.Id = record.Id == Guid.Empty ? Guid.NewGuid() : record.Id;

        if (!MedicalRecordValidator.Validate(record, out string error))
            return (false, error);

        await _medicalRepository.AddAsync(record);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> UpdateAsync(MedicalRecord record)
    {
        var existing = await GetByIdAsync(record.Id);
        if (existing == null)
            return (false, "Record not found.");

        record.DogId = existing.DogId;

        if (!MedicalRecordValidator.Validate(record, out string error))
            return (false, error);

        await _medicalRepository.UpdateAsync(record);
        return (true, string.Empty);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await GetByIdAsync(id);
        if (existing == null)
            return false;

        await _medicalRepository.DeleteAsync(id);
        return true;
    }
}
