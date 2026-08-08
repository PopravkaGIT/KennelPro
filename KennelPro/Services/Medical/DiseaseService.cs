using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;
using KennelPro.Services.Dogs;
using KennelPro.Validators;

namespace KennelPro.Services.Medical;

public class DiseaseService
{
    private readonly IDiseaseRepository _diseaseRepository;
    private readonly DogService _dogService;

    public DiseaseService(
        IDiseaseRepository diseaseRepository,
        DogService dogService)
    {
        _diseaseRepository = diseaseRepository;
        _dogService = dogService;
    }

    public async Task<List<Disease>> GetForDogAsync(Guid dogId)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(dogId) == null)
            return new List<Disease>();

        return (await _diseaseRepository.GetByDogIdAsync(dogId)).ToList();
    }

    public async Task<Disease?> GetByIdAsync(Guid id)
    {
        var item = await _diseaseRepository.GetByIdAsync(id);
        if (item == null)
            return null;

        if (await _dogService.GetDogForCurrentKennelAsync(item.DogId) == null)
            return null;

        return item;
    }

    public async Task<(bool Success, string Error)> AddAsync(Disease disease)
    {
        if (await _dogService.GetDogForCurrentKennelAsync(disease.DogId) == null)
            return (false, "Dog not found.");

        disease.Id = disease.Id == Guid.Empty ? Guid.NewGuid() : disease.Id;

        if (!DiseaseValidator.Validate(disease, out string error))
            return (false, error);

        await _diseaseRepository.AddAsync(disease);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> UpdateAsync(Disease disease)
    {
        var existing = await GetByIdAsync(disease.Id);
        if (existing == null)
            return (false, "Disease not found.");

        disease.DogId = existing.DogId;

        if (!DiseaseValidator.Validate(disease, out string error))
            return (false, error);

        await _diseaseRepository.UpdateAsync(disease);
        return (true, string.Empty);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await GetByIdAsync(id);
        if (existing == null)
            return false;

        await _diseaseRepository.DeleteAsync(id);
        return true;
    }
}
