using System.Net.Http.Json;

namespace KennelPro.Services.Api;

public class HttpService
{
    private readonly HttpClient _httpClient;

    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<T>(url);
        }
        catch
        {
            return default;
        }
    }

    public async Task<string?> GetStringAsync(string url)
    {
        try
        {
            return await _httpClient.GetStringAsync(url);
        }
        catch
        {
            return null;
        }
    }
}