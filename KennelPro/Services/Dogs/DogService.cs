using KennelPro.Interfaces.Dogs;
using KennelPro.Models.Dogs;

namespace KennelPro.Services.Dogs;

public class DogService
{
    private readonly IDogRepository _dogRepository;


    public DogService(IDogRepository dogRepository)
    {
        _dogRepository = dogRepository;
    }


    public async Task<List<Dog>> GetDogsAsync()
    {
        return await _dogRepository.GetAllAsync();
    }


    public async Task<Dog?> GetDogAsync(Guid id)
    {
        return await _dogRepository.GetByIdAsync(id);
    }


    public async Task AddDogAsync(Dog dog)
    {
        await _dogRepository.AddAsync(dog);
    }


    public async Task UpdateDogAsync(Dog dog)
    {
        await _dogRepository.UpdateAsync(dog);
    }


    public async Task DeleteDogAsync(Guid id)
    {
        await _dogRepository.DeleteAsync(id);
    }
}