using KennelPro.Helpers;
using KennelPro.Models.Litters;

namespace KennelPro.Validators;

public static class LitterValidator
{
    public static bool Validate(Litter litter, out string error)
    {
        if (!ValidationHelper.HasText(litter.Name))
        {
            error = "Litter name is required.";
            return false;
        }

        if (litter.BirthDate > DateTime.Today)
        {
            error = "Birth date cannot be in the future.";
            return false;
        }

        if (litter.MotherDogId == Guid.Empty)
        {
            error = "Mother dog is required.";
            return false;
        }

        if (litter.FatherDogId == Guid.Empty)
        {
            error = "Father dog is required.";
            return false;
        }
        if (litter.FatherDogId == litter.MotherDogId) { error = "Parents must be different dogs."; return false; }

        error = string.Empty;
        return true;
    }
}
