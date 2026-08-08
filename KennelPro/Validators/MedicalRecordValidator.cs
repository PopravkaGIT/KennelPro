using KennelPro.Models.Medical;

namespace KennelPro.Validators;

public static class MedicalRecordValidator
{
    public static bool Validate(MedicalRecord record, out string error)
    {
        if (record.DogId == Guid.Empty)
        {
            error = "Dog is required.";
            return false;
        }

        if (record.Date > DateTime.Today)
        {
            error = "Date cannot be in the future.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(record.Notes))
        {
            error = "Notes are required.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}
