using KennelPro.Helpers;
using KennelPro.Models.Medical;

namespace KennelPro.Validators;

public static class MedicationValidator
{
    public static bool Validate(Medication medication, out string error)
    {
        if (medication.DogId == Guid.Empty)
        {
            error = "Dog is required.";
            return false;
        }

        if (!ValidationHelper.HasText(medication.Name))
        {
            error = "Medication name is required.";
            return false;
        }

        if (medication.StartDate > DateTime.Today)
        {
            error = "Start date cannot be in the future.";
            return false;
        }

        if (medication.EndDate.HasValue && medication.EndDate.Value.Date < medication.StartDate.Date)
        {
            error = "End date cannot be before start date.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}
