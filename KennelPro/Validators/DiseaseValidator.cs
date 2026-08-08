using KennelPro.Helpers;
using KennelPro.Models.Medical;

namespace KennelPro.Validators;

public static class DiseaseValidator
{
    public static bool Validate(Disease disease, out string error)
    {
        if (disease.DogId == Guid.Empty)
        {
            error = "Dog is required.";
            return false;
        }

        if (!ValidationHelper.HasText(disease.Name))
        {
            error = "Disease name is required.";
            return false;
        }

        if (disease.StartDate > DateTime.Today)
        {
            error = "Start date cannot be in the future.";
            return false;
        }

        if (disease.RecoveryDate.HasValue && disease.RecoveryDate.Value.Date < disease.StartDate.Date)
        {
            error = "Recovery date cannot be before start date.";
            return false;
        }

        if (!ValidationHelper.HasText(disease.Status))
        {
            error = "Status is required.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}
