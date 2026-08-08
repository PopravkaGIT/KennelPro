using Microsoft.Maui.Storage;

namespace KennelPro.Helpers;

public static class SessionManager
{
    private const string UserIdKey = "CurrentUserId";

    public static void SaveUser(Guid userId)
    {
        Preferences.Default.Set(UserIdKey, userId.ToString());
    }

    public static Guid? GetCurrentUserId()
    {
        string value = Preferences.Default.Get(UserIdKey, string.Empty);

        if (string.IsNullOrWhiteSpace(value))
            return null;

        return Guid.TryParse(value, out Guid id)
            ? id
            : null;
    }

    public static bool IsLoggedIn()
    {
        return GetCurrentUserId() != null;
    }

    public static void Logout()
    {
        Preferences.Default.Remove(UserIdKey);
    }
}