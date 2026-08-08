using System.Windows.Input;
using KennelPro.Models.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class VaccinationEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly VaccinationService _vaccinationService;

    private Guid _dogId;
    private Guid? _vaccinationId;

    public VaccinationEditViewModel(VaccinationService vaccinationService)
    {
        _vaccinationService = vaccinationService;

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    private string _pageTitle = "Add Vaccination";
    public string PageTitle
    {
        get => _pageTitle;
        set => SetProperty(ref _pageTitle, value);
    }

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private DateTime _vaccinationDate = DateTime.Today;
    public DateTime VaccinationDate
    {
        get => _vaccinationDate;
        set => SetProperty(ref _vaccinationDate, value);
    }

    private DateTime? _revaccinationDate;
    public DateTime? RevaccinationDate
    {
        get => _revaccinationDate;
        set => SetProperty(ref _revaccinationDate, value);
    }

    private bool _hasRevaccinationDate;
    public bool HasRevaccinationDate
    {
        get => _hasRevaccinationDate;
        set
        {
            if (SetProperty(ref _hasRevaccinationDate, value) && !value)
                RevaccinationDate = null;
        }
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("dogId", out var dogValue))
        {
            var dogText = dogValue?.ToString();
            if (!string.IsNullOrWhiteSpace(dogText) && Guid.TryParse(dogText, out var dogId))
                _dogId = dogId;
        }

        if (query.TryGetValue("vaccinationId", out var vaccinationValue))
        {
            var vaccinationText = vaccinationValue?.ToString();
            if (!string.IsNullOrWhiteSpace(vaccinationText) && Guid.TryParse(vaccinationText, out var vaccinationId))
                _vaccinationId = vaccinationId;
        }
    }

    public async Task InitializeAsync()
    {
        if (_vaccinationId == null)
        {
            PageTitle = "Add Vaccination";
            return;
        }

        PageTitle = "Edit Vaccination";

        var vaccination = await _vaccinationService.GetByIdAsync(_vaccinationId.Value);
        if (vaccination == null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Vaccination not found.",
                "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        Name = vaccination.Name;
        VaccinationDate = vaccination.VaccinationDate;
        HasRevaccinationDate = vaccination.RevaccinationDate.HasValue;
        RevaccinationDate = vaccination.RevaccinationDate;
    }

    private async Task SaveAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var vaccination = new Vaccination
            {
                Id = _vaccinationId ?? Guid.Empty,
                DogId = _dogId,
                Name = Name.Trim(),
                VaccinationDate = VaccinationDate.Date,
                RevaccinationDate = HasRevaccinationDate ? RevaccinationDate?.Date : null
            };

            (bool success, string error) = _vaccinationId == null
                ? await _vaccinationService.AddAsync(vaccination)
                : await _vaccinationService.UpdateAsync(vaccination);

            if (!success)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    string.IsNullOrWhiteSpace(error) ? "Could not save vaccination." : error,
                    "OK");
                return;
            }

            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
