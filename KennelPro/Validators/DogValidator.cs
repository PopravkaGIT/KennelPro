using KennelPro.Helpers;
using KennelPro.Models.Dogs;

namespace KennelPro.Validators;

public static class DogValidator
{
    public static bool Validate(Dog dog, out string error)
    {
        if (!ValidationHelper.HasText(dog.Name))
        {
            error = "Dog name is required.";
            return false;
        }

        if (dog.BirthDate > DateTime.Today)
        {
            error = "Birth date cannot be in the future.";
            return false;
        }

        if (dog.BreedId == Guid.Empty)
        {
            error = "Breed is required.";
            return false;
        }

        if (dog.KennelId == Guid.Empty)
        {
            error = "Kennel is required.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}