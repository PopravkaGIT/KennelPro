using KennelPro.Helpers;
using KennelPro.Models.Medical;

namespace KennelPro.ViewModels.Medical;

public class VaccinationListItem
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string VaccinationDateText { get; init; } = string.Empty;

    public string RevaccinationDateText { get; init; } = string.Empty;

    public static VaccinationListItem FromVaccination(Vaccination vaccination)
    {
        return new VaccinationListItem
        {
            Id = vaccination.Id,
            Name = vaccination.Name,
            VaccinationDateText = DateHelper.FormatDate(vaccination.VaccinationDate),
            RevaccinationDateText = DateHelper.FormatDate(vaccination.RevaccinationDate)
        };
    }
}
