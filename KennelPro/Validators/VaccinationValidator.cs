using KennelPro.Helpers;
using KennelPro.Models.Medical;

namespace KennelPro.Validators;

public static class VaccinationValidator
{
    public static bool Validate(Vaccination vaccination, out string error)
    {
        if (vaccination.DogId == Guid.Empty)
        {
            error = "Dog is required.";
            return false;
        }

        if (!ValidationHelper.HasText(vaccination.Name))
        {
            error = "Vaccination name is required.";
            return false;
        }

        if (vaccination.VaccinationDate > DateTime.Today)
        {
            error = "Vaccination date cannot be in the future.";
            return false;
        }

        if (vaccination.RevaccinationDate.HasValue &&
            vaccination.RevaccinationDate.Value.Date < vaccination.VaccinationDate.Date)
        {
            error = "Revaccination date cannot be before vaccination date.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}
