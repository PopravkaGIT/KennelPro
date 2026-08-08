using KennelPro.Helpers;
using KennelPro.Models.Medical;

namespace KennelPro.ViewModels.Medical;

public class MedicationListItem
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string DosageText { get; init; } = string.Empty;

    public string DateText { get; init; } = string.Empty;

    public static MedicationListItem FromMedication(Medication medication)
    {
        return new MedicationListItem
        {
            Id = medication.Id,
            Name = medication.Name,
            DosageText = string.IsNullOrWhiteSpace(medication.Dosage) ? "-" : medication.Dosage,
            DateText = DateHelper.FormatDate(medication.StartDate == default ? medication.Date : medication.StartDate)
        };
    }
}

internal static class MedicationFieldMapper
{
    public static (string Dosage, string Frequency, DateTime? EndDate) ParseDescription(string? description)
    {
        var dosage = string.Empty;
        var frequency = string.Empty;
        DateTime? endDate = null;

        if (string.IsNullOrWhiteSpace(description))
            return (dosage, frequency, endDate);

        foreach (var line in description.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith("Dosage:", StringComparison.OrdinalIgnoreCase))
                dosage = trimmed["Dosage:".Length..].Trim();
            else if (trimmed.StartsWith("Frequency:", StringComparison.OrdinalIgnoreCase))
                frequency = trimmed["Frequency:".Length..].Trim();
            else if (trimmed.StartsWith("End:", StringComparison.OrdinalIgnoreCase))
            {
                var dateText = trimmed["End:".Length..].Trim();
                if (DateTime.TryParse(dateText, out var parsed))
                    endDate = parsed.Date;
            }
        }

        return (dosage, frequency, endDate);
    }

    public static string BuildDescription(string dosage, string frequency, DateTime? endDate)
    {
        var lines = new List<string>();

        if (!string.IsNullOrWhiteSpace(dosage))
            lines.Add($"Dosage: {dosage.Trim()}");

        if (!string.IsNullOrWhiteSpace(frequency))
            lines.Add($"Frequency: {frequency.Trim()}");

        if (endDate.HasValue)
            lines.Add($"End: {endDate.Value:dd.MM.yyyy}");

        return lines.Count == 0 ? string.Empty : string.Join('\n', lines);
    }
}
