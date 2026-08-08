using System.Windows.Input;
using KennelPro.Models.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class ParasiteTreatmentEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ParasiteService _parasiteService;

    private Guid _dogId;
    private Guid? _treatmentId;

    public ParasiteTreatmentEditViewModel(ParasiteService parasiteService)
    {
        _parasiteService = parasiteService;

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    private string _pageTitle = "Add Treatment";
    public string PageTitle
    {
        get => _pageTitle;
        set => SetProperty(ref _pageTitle, value);
    }

    private string _productName = string.Empty;
    public string ProductName
    {
        get => _productName;
        set => SetProperty(ref _productName, value);
    }

    private DateTime _date = DateTime.Today;
    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    private DateTime? _nextTreatmentDate;
    public DateTime? NextTreatmentDate
    {
        get => _nextTreatmentDate;
        set => SetProperty(ref _nextTreatmentDate, value);
    }

    private bool _hasNextTreatmentDate;
    public bool HasNextTreatmentDate
    {
        get => _hasNextTreatmentDate;
        set
        {
            if (SetProperty(ref _hasNextTreatmentDate, value) && !value)
                NextTreatmentDate = null;
        }
    }

    private string _notes = string.Empty;
    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
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

        if (query.TryGetValue("treatmentId", out var treatmentValue))
        {
            var treatmentText = treatmentValue?.ToString();
            if (!string.IsNullOrWhiteSpace(treatmentText) && Guid.TryParse(treatmentText, out var treatmentId))
                _treatmentId = treatmentId;
        }
    }

    public async Task InitializeAsync()
    {
        if (_treatmentId == null)
        {
            PageTitle = "Add Treatment";
            return;
        }

        PageTitle = "Edit Treatment";

        var treatment = await _parasiteService.GetByIdAsync(_treatmentId.Value);
        if (treatment == null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Treatment not found.",
                "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        ProductName = treatment.ProductName;
        Date = treatment.Date;
        HasNextTreatmentDate = treatment.NextTreatmentDate.HasValue;
        NextTreatmentDate = treatment.NextTreatmentDate;
        Notes = treatment.Notes ?? string.Empty;
    }

    private async Task SaveAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var treatment = new ParasiteTreatment
            {
                Id = _treatmentId ?? Guid.Empty,
                DogId = _dogId,
                ProductName = ProductName.Trim(),
                Date = Date.Date,
                NextTreatmentDate = HasNextTreatmentDate ? NextTreatmentDate?.Date : null,
                Notes = string.IsNullOrWhiteSpace(Notes) ? null : Notes.Trim()
            };

            (bool success, string error) = _treatmentId == null
                ? await _parasiteService.AddAsync(treatment)
                : await _parasiteService.UpdateAsync(treatment);

            if (!success)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    string.IsNullOrWhiteSpace(error) ? "Could not save treatment." : error,
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
