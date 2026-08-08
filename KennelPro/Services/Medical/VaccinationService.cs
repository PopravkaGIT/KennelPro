using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using KennelPro.Services.Dogs;
using KennelPro.Validators;

namespace KennelPro.Services.Medical;

public class VaccinationService
{
    private readonly IVaccinationRepository _vaccinationRepository;
    private readonly DogService _dogService;

    public VaccinationService(
        IVaccinationRepository vaccinationRepository,
        DogService dogService)
    {
        _vaccinationRepository = vaccinationRepository;
        _dogService = dogService;
    }

    public async Task<List<Vaccination>> GetForDogAsync(Guid dogId)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(dogId) == null)
            return new List<Vaccination>();

        return (await _vaccinationRepository.GetByDogIdAsync(dogId)).ToList();
    }

    public async Task<Vaccination?> GetByIdAsync(Guid id)
    {
        var item = await _vaccinationRepository.GetByIdAsync(id);
        if (item == null)
            return null;

        if (await _dogService.GetDogForCurrentKennelAsync(item.DogId) == null)
            return null;

        return item;
    }

    public async Task<(bool Success, string Error)> AddAsync(Vaccination vaccination)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(vaccination.DogId) == null)
            return (false, "Dog not found.");

        vaccination.Id = vaccination.Id == Guid.Empty ? Guid.NewGuid() : vaccination.Id;

        if (!VaccinationValidator.Validate(vaccination, out string error))
            return (false, error);

        await _vaccinationRepository.AddAsync(vaccination);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> UpdateAsync(Vaccination vaccination)
    {
        var existing = await GetByIdAsync(vaccination.Id);
        if (existing == null)
            return (false, "Vaccination not found.");

        vaccination.DogId = existing.DogId;

        if (!VaccinationValidator.Validate(vaccination, out string error))
            return (false, error);

        await _vaccinationRepository.UpdateAsync(vaccination);
        return (true, string.Empty);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await GetByIdAsync(id);
        if (existing == null)
            return false;

        await _vaccinationRepository.DeleteAsync(id);
        return true;
    }
}
