using KennelPro.Interfaces.Dogs;
using KennelPro.Models.Dogs;

namespace KennelPro.Services.Dogs;

public class BreedService
{
    private readonly IBreedRepository _breedRepository;


    public BreedService(IBreedRepository breedRepository)
    {
        _breedRepository = breedRepository;
    }


    public async Task<List<Breed>> GetAllBreedsAsync()
    {
        return await _breedRepository.GetAllAsync();
    }


    public async Task<Breed?> GetBreedAsync(Guid id)
    {
        return await _breedRepository.GetByIdAsync(id);
    }


    public async Task AddBreedAsync(Breed breed)
    {
        await _breedRepository.AddAsync(breed);
    }


    public async Task UpdateBreedAsync(Breed breed)
    {
        await _breedRepository.UpdateAsync(breed);
    }


    public async Task DeleteBreedAsync(Guid id)
    {
        await _breedRepository.DeleteAsync(id);
    }
}