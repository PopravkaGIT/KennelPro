using KennelPro.Helpers;
using KennelPro.Models.Authentication;

namespace KennelPro.Validators;

public static class UserValidator
{
    public static bool Validate(User user, out string error)
    {
        if (!ValidationHelper.IsEmail(user.Email))
        {
            error = "Invalid email address.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}