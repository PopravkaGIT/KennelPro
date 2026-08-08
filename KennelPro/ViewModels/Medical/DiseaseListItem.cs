using KennelPro.Models.Medical;

namespace KennelPro.ViewModels.Medical;

public class DiseaseListItem
{
    public Guid Id { get; init; }

    public string Diagnosis { get; init; } = string.Empty;

    public string StatusText { get; init; } = string.Empty;

    public string SymptomsPreview { get; init; } = string.Empty;

    public static DiseaseListItem FromDisease(Disease disease)
    {
        return new DiseaseListItem
        {
            Id = disease.Id,
            Diagnosis = disease.Name,
            StatusText = string.IsNullOrWhiteSpace(disease.Status) ? "-" : disease.Status,
            SymptomsPreview = string.IsNullOrWhiteSpace(disease.Symptoms) ? "-" : disease.Symptoms
        };
    }
}

internal static class DiseaseFieldMapper
{
    public static (string Status, DateTime? StartDate, DateTime? RecoveryDate) ParseNotes(string? notes)
    {
        var status = string.Empty;
        DateTime? startDate = null;
        DateTime? recoveryDate = null;

        if (string.IsNullOrWhiteSpace(notes))
            return (status, startDate, recoveryDate);

        foreach (var line in notes.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith("Status:", StringComparison.OrdinalIgnoreCase))
                status = trimmed["Status:".Length..].Trim();
            else if (trimmed.StartsWith("Start:", StringComparison.OrdinalIgnoreCase))
            {
                var dateText = trimmed["Start:".Length..].Trim();
                if (DateTime.TryParse(dateText, out var parsed))
                    startDate = parsed.Date;
            }
            else if (trimmed.StartsWith("Recovery:", StringComparison.OrdinalIgnoreCase))
            {
                var dateText = trimmed["Recovery:".Length..].Trim();
                if (dateText != "-" && DateTime.TryParse(dateText, out var parsed))
                    recoveryDate = parsed.Date;
            }
        }

        return (status, startDate, recoveryDate);
    }

    public static string BuildNotes(string status, DateTime? startDate, DateTime? recoveryDate)
    {
        var lines = new List<string>
        {
            $"Status: {(string.IsNullOrWhiteSpace(status) ? "Active" : status.Trim())}",
            $"Start: {(startDate.HasValue ? startDate.Value.ToString("dd.MM.yyyy") : "-")}",
            $"Recovery: {(recoveryDate.HasValue ? recoveryDate.Value.ToString("dd.MM.yyyy") : "-")}"
        };

        return string.Join('\n', lines);
    }
}
