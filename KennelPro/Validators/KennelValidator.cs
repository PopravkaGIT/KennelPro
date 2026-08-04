using KennelPro.Helpers;
using KennelPro.Models.Kennels;

namespace KennelPro.Validators;

public static class KennelValidator
{
    public static bool Validate(Kennel kennel, out string error)
    {
        if (!ValidationHelper.HasText(kennel.Name))
        {
            error = "Kennel name is required.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}