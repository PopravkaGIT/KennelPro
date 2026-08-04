namespace KennelPro.Helpers;

public static class FileHelper
{
    public static bool Exists(string path)
    {
        return File.Exists(path);
    }

    public static string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }

    public static string GetExtension(string path)
    {
        return Path.GetExtension(path);
    }

    public static long GetFileSize(string path)
    {
        if (!File.Exists(path))
            return 0;

        return new FileInfo(path).Length;
    }
}