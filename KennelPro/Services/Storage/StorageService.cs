namespace KennelPro.Services.Storage;

public class StorageService
{
    public string GetDatabasePath()
    {
        return Path.Combine(
            FileSystem.AppDataDirectory,
            "KennelPro.db");
    }
}