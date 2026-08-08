using System.Windows.Input;
using KennelPro.Models.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class MedicationEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly MedicationService _medicationService;

    private Guid _dogId;
    private Guid? _medicationId;

    public MedicationEditViewModel(MedicationService medicationService)
    {
        _medicationService = medicationService;

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    private string _pageTitle = "Add Medication";
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

    private string _dosage = string.Empty;
    public string Dosage
    {
        get => _dosage;
        set => SetProperty(ref _dosage, value);
    }

    private string _frequency = string.Empty;
    public string Frequency
    {
        get => _frequency;
        set => SetProperty(ref _frequency, value);
    }

    private DateTime _startDate = DateTime.Today;
    public DateTime StartDate
    {
        get => _startDate;
        set => SetProperty(ref _startDate, value);
    }

    private DateTime? _endDate;
    public DateTime? EndDate
    {
        get => _endDate;
        set => SetProperty(ref _endDate, value);
    }

    private bool _hasEndDate;
    public bool HasEndDate
    {
        get => _hasEndDate;
        set
        {
            if (SetProperty(ref _hasEndDate, value) && !value)
                EndDate = null;
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

        if (query.TryGetValue("medicationId", out var medicationValue))
        {
            var medicationText = medicationValue?.ToString();
            if (!string.IsNullOrWhiteSpace(medicationText) && Guid.TryParse(medicationText, out var medicationId))
                _medicationId = medicationId;
        }
    }

    public async Task InitializeAsync()
    {
        if (_medicationId == null)
        {
            PageTitle = "Add Medication";
            return;
        }

        PageTitle = "Edit Medication";

        var medication = await _medicationService.GetByIdAsync(_medicationId.Value);
        if (medication == null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Medication not found.",
                "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        Name = medication.Name;
        StartDate = medication.StartDate == default ? medication.Date : medication.StartDate;
        Dosage = medication.Dosage ?? string.Empty;
        Frequency = medication.Frequency ?? string.Empty;
        HasEndDate = medication.EndDate.HasValue;
        EndDate = medication.EndDate;
    }

    private async Task SaveAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var medication = new Medication
            {
                Id = _medicationId ?? Guid.Empty,
                DogId = _dogId,
                Name = Name.Trim(),
                Dosage = string.IsNullOrWhiteSpace(Dosage) ? null : Dosage.Trim(),
                Frequency = string.IsNullOrWhiteSpace(Frequency) ? null : Frequency.Trim(),
                StartDate = StartDate.Date,
                EndDate = HasEndDate ? EndDate?.Date : null,
                // Preserves compatibility for data produced by earlier builds.
                Date = StartDate.Date
            };

            (bool success, string error) = _medicationId == null
                ? await _medicationService.AddAsync(medication)
                : await _medicationService.UpdateAsync(medication);

            if (!success)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    string.IsNullOrWhiteSpace(error) ? "Could not save medication." : error,
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
