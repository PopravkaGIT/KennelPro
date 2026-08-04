namespace KennelPro.Services.Settings;

public class SettingsService
{
    public async Task SetAsync(string key, string value)
    {
        Preferences.Default.Set(key, value);
        await Task.CompletedTask;
    }

    public async Task<string?> GetAsync(string key)
    {
        return await Task.FromResult(Preferences.Default.Get<string?>(key, null));
    }

    public async Task RemoveAsync(string key)
    {
        Preferences.Default.Remove(key);
        await Task.CompletedTask;
    }

    public async Task ClearAsync()
    {
        Preferences.Default.Clear();
        await Task.CompletedTask;
    }
}