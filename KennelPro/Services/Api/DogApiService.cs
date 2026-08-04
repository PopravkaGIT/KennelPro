using KennelPro.Services.Api;

namespace KennelPro.Services.Api;

public class DogApiService
{
    private readonly HttpService _httpService;

    public DogApiService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<string?> GetBreedsAsync()
    {
        return await _httpService.GetStringAsync(
            "https://dog.ceo/api/breeds/list/all");
    }
}