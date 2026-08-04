using System.Net.Http.Json;

namespace KennelPro.Services.Api;

public class DogApiService
{
    private readonly HttpService _httpService;

    public DogApiService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<string> GetBreedDescriptionAsync(string breedName)
    {
        // TODO: Подключить реальное API
        await Task.Delay(1);

        return $"Описание породы {breedName}";
    }

    public async Task<List<string>> GetBreedImagesAsync(string breedName)
    {
        // TODO: Подключить API фотографий

        await Task.Delay(1);

        return new List<string>();
    }

    public async Task<List<string>> GetBreedCharacteristicsAsync(string breedName)
    {
        // TODO: Подключить API характеристик

        await Task.Delay(1);

        return new List<string>();
    }
}