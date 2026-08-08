using KennelPro.Interfaces.Dogs;
using KennelPro.Models.Dogs;
using KennelPro.Services.Authentication;
using KennelPro.Validators;

namespace KennelPro.Services.Dogs;

public class DogService
{
    private readonly IDogRepository _dogRepository;
    private readonly AuthenticationService _authenticationService;

    public DogService(
        IDogRepository dogRepository,
        AuthenticationService authenticationService)
    {
        _dogRepository = dogRepository;
        _authenticationService = authenticationService;
    }

    public async Task<Guid?> GetCurrentKennelIdAsync()
    {
        var user = await _authenticationService.GetCurrentUserAsync();
        return user?.KennelId;
    }

    public async Task<List<Dog>> GetDogsForCurrentKennelAsync()
    {
        var kennelId = await GetCurrentKennelIdAsync();
        if (kennelId == null)
            return new List<Dog>();

        return await _dogRepository.GetByKennelIdAsync(kennelId.Value);
    }

    public async Task<Dog?> GetDogForCurrentKennelAsync(Guid id)
    {
        var kennelId = await GetCurrentKennelIdAsync();
        if (kennelId == null)
            return null;

        var dog = await _dogRepository.GetByIdAsync(id);
        if (dog == null || dog.KennelId != kennelId.Value)
            return null;

        return dog;
    }

    public async Task<(bool Success, string Error)> AddDogAsync(Dog dog)
    {
        var kennelId = await GetCurrentKennelIdAsync();
        if (kennelId == null)
            return (false, "You must be signed in.");

        dog.KennelId = kennelId.Value;
        dog.Id = dog.Id == Guid.Empty ? Guid.NewGuid() : dog.Id;
        dog.CreatedAt = DateTime.UtcNow;

        if (!DogValidator.Validate(dog, out string error))
            return (false, error);

        await _dogRepository.AddAsync(dog);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> UpdateDogAsync(Dog dog)
    {
        var kennelId = await GetCurrentKennelIdAsync();
        if (kennelId == null)
            return (false, "You must be signed in.");

        var existing = await _dogRepository.GetByIdAsync(dog.Id);
        if (existing == null || existing.KennelId != kennelId.Value)
            return (false, "Dog not found.");

        dog.KennelId = kennelId.Value;
        dog.CreatedAt = existing.CreatedAt;

        if (!DogValidator.Validate(dog, out string error))
            return (false, error);

        await _dogRepository.UpdateAsync(dog);
        return (true, string.Empty);
    }

    public async Task<bool> DeleteDogAsync(Guid id)
    {
        var dog = await GetDogForCurrentKennelAsync(id);
        if (dog == null)
            return false;

        await _dogRepository.DeleteAsync(id);
        return true;
    }
}
