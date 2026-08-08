using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using KennelPro.Services.Dogs;
using KennelPro.Validators;

namespace KennelPro.Services.Medical;

public class MedicationService
{
    private readonly IMedicationRepository _medicationRepository;
    private readonly DogService _dogService;

    public MedicationService(
        IMedicationRepository medicationRepository,
        DogService dogService)
    {
        _medicationRepository = medicationRepository;
        _dogService = dogService;
    }

    public async Task<List<Medication>> GetForDogAsync(Guid dogId)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(dogId) == null)
            return new List<Medication>();

        return (await _medicationRepository.GetByDogIdAsync(dogId)).ToList();
    }

    public async Task<Medication?> GetByIdAsync(Guid id)
    {
        var item = await _medicationRepository.GetByIdAsync(id);
        if (item == null)
            return null;

        if (await _dogService.GetDogForCurrentKennelAsync(item.DogId) == null)
            return null;

        return item;
    }

    public async Task<(bool Success, string Error)> AddAsync(Medication medication)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(medication.DogId) == null)
            return (false, "Dog not found.");

        medication.Id = medication.Id == Guid.Empty ? Guid.NewGuid() : medication.Id;

        if (!MedicationValidator.Validate(medication, out string error))
            return (false, error);

        await _medicationRepository.AddAsync(medication);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> UpdateAsync(Medication medication)
    {
        var existing = await GetByIdAsync(medication.Id);
        if (existing == null)
            return (false, "Medication not found.");

        medication.DogId = existing.DogId;

        if (!MedicationValidator.Validate(medication, out string error))
            return (false, error);

        await _medicationRepository.UpdateAsync(medication);
        return (true, string.Empty);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await GetByIdAsync(id);
        if (existing == null)
            return false;

        await _medicationRepository.DeleteAsync(id);
        return true;
    }
}
