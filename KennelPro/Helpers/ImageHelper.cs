namespace KennelPro.Helpers;

public static class ImageHelper
{
    public static bool IsImage(string fileName)
    {
        string extension = Path.GetExtension(fileName).ToLower();

        return extension is ".jpg"
            or ".jpeg"
            or ".png"
            or ".bmp"
            or ".gif"
            or ".webp";
    }

    public static string DefaultDogImage =>
        "dog_placeholder.png";
}