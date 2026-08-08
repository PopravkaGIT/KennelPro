using KennelPro.Helpers;
using KennelPro.Models.Medical;

namespace KennelPro.Validators;

public static class ParasiteTreatmentValidator
{
    public static bool Validate(ParasiteTreatment treatment, out string error)
    {
        if (treatment.DogId == Guid.Empty)
        {
            error = "Dog is required.";
            return false;
        }

        if (!ValidationHelper.HasText(treatment.ProductName))
        {
            error = "Product name is required.";
            return false;
        }

        if (treatment.Date > DateTime.Today)
        {
            error = "Treatment date cannot be in the future.";
            return false;
        }

        if (treatment.NextTreatmentDate.HasValue &&
            treatment.NextTreatmentDate.Value.Date < treatment.Date.Date)
        {
            error = "Next treatment date cannot be before treatment date.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}
