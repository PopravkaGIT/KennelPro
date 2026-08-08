using KennelPro.Helpers;
using KennelPro.Models.Medical;

namespace KennelPro.ViewModels.Medical;

public class ParasiteTreatmentListItem
{
    public Guid Id { get; init; }

    public string ProductName { get; init; } = string.Empty;

    public string DateText { get; init; } = string.Empty;

    public string NextTreatmentDateText { get; init; } = string.Empty;

    public static ParasiteTreatmentListItem FromTreatment(ParasiteTreatment treatment)
    {
        return new ParasiteTreatmentListItem
        {
            Id = treatment.Id,
            ProductName = treatment.ProductName,
            DateText = DateHelper.FormatDate(treatment.Date),
            NextTreatmentDateText = DateHelper.FormatDate(treatment.NextTreatmentDate)
        };
    }
}
