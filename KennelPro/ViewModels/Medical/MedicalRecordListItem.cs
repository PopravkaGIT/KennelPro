using KennelPro.Helpers;
using KennelPro.Models.Medical;

namespace KennelPro.ViewModels.Medical;

public class MedicalRecordListItem
{
    public Guid Id { get; init; }

    public string DateText { get; init; } = string.Empty;

    public string NotesPreview { get; init; } = string.Empty;

    public static MedicalRecordListItem FromRecord(MedicalRecord record)
    {
        return new MedicalRecordListItem
        {
            Id = record.Id,
            DateText = DateHelper.FormatDate(record.Date),
            NotesPreview = string.IsNullOrWhiteSpace(record.Notes) ? "-" : record.Notes
        };
    }
}
