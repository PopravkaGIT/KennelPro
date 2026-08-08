using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using KennelPro.Services.Dogs;
using KennelPro.Validators;

namespace KennelPro.Services.Medical;

public class ParasiteService
{
    private readonly IParasiteTreatmentRepository _parasiteRepository;
    private readonly DogService _dogService;

    public ParasiteService(
        IParasiteTreatmentRepository parasiteRepository,
        DogService dogService)
    {
        _parasiteRepository = parasiteRepository;
        _dogService = dogService;
    }

    public async Task<List<ParasiteTreatment>> GetForDogAsync(Guid dogId)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(dogId) == null)
            return new List<ParasiteTreatment>();

        return (await _parasiteRepository.GetByDogIdAsync(dogId)).ToList();
    }

    public async Task<ParasiteTreatment?> GetByIdAsync(Guid id)
    {
        var item = await _parasiteRepository.GetByIdAsync(id);
        if (item == null)
            return null;

        if (await _dogService.GetDogForCurrentKennelAsync(item.DogId) == null)
            return null;

        return item;
    }

    public async Task<(bool Success, string Error)> AddAsync(ParasiteTreatment treatment)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(treatment.DogId) == null)
            return (false, "Dog not found.");

        treatment.Id = treatment.Id == Guid.Empty ? Guid.NewGuid() : treatment.Id;

        if (!ParasiteTreatmentValidator.Validate(treatment, out string error))
            return (false, error);

        await _parasiteRepository.AddAsync(treatment);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> UpdateAsync(ParasiteTreatment treatment)
    {
        var existing = await GetByIdAsync(treatment.Id);
        if (existing == null)
            return (false, "Treatment not found.");

        treatment.DogId = existing.DogId;

        if (!ParasiteTreatmentValidator.Validate(treatment, out string error))
            return (false, error);

        await _parasiteRepository.UpdateAsync(treatment);
        return (true, string.Empty);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await GetByIdAsync(id);
        if (existing == null)
            return false;

        await _parasiteRepository.DeleteAsync(id);
        return true;
    }
}
