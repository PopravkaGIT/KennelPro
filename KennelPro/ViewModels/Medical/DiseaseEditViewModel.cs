using System.Collections.ObjectModel;
using System.Windows.Input;
using KennelPro.Models.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class DiseaseEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly DiseaseService _diseaseService;

    private Guid _dogId;
    private Guid? _diseaseId;

    public DiseaseEditViewModel(DiseaseService diseaseService)
    {
        _diseaseService = diseaseService;

        StatusOptions = new ObservableCollection<string> { "Active", "Recovered", "Chronic" };

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ObservableCollection<string> StatusOptions { get; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    private string _pageTitle = "Add Disease";
    public string PageTitle
    {
        get => _pageTitle;
        set => SetProperty(ref _pageTitle, value);
    }

    private string _diagnosis = string.Empty;
    public string Diagnosis
    {
        get => _diagnosis;
        set => SetProperty(ref _diagnosis, value);
    }

    private string _symptoms = string.Empty;
    public string Symptoms
    {
        get => _symptoms;
        set => SetProperty(ref _symptoms, value);
    }

    private DateTime _startDate = DateTime.Today;
    public DateTime StartDate
    {
        get => _startDate;
        set => SetProperty(ref _startDate, value);
    }

    private DateTime? _recoveryDate;
    public DateTime? RecoveryDate
    {
        get => _recoveryDate;
        set => SetProperty(ref _recoveryDate, value);
    }

    private bool _hasRecoveryDate;
    public bool HasRecoveryDate
    {
        get => _hasRecoveryDate;
        set
        {
            if (SetProperty(ref _hasRecoveryDate, value) && !value)
                RecoveryDate = null;
        }
    }

    private string _selectedStatus = "Active";
    public string SelectedStatus
    {
        get => _selectedStatus;
        set => SetProperty(ref _selectedStatus, value);
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

        if (query.TryGetValue("diseaseId", out var diseaseValue))
        {
            var diseaseText = diseaseValue?.ToString();
            if (!string.IsNullOrWhiteSpace(diseaseText) && Guid.TryParse(diseaseText, out var diseaseId))
                _diseaseId = diseaseId;
        }
    }

    public async Task InitializeAsync()
    {
        if (_diseaseId == null)
        {
            PageTitle = "Add Disease";
            return;
        }

        PageTitle = "Edit Disease";

        var disease = await _diseaseService.GetByIdAsync(_diseaseId.Value);
        if (disease == null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Disease not found.",
                "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        Diagnosis = disease.Name;
        Symptoms = disease.Symptoms ?? disease.AllergyInfo ?? string.Empty;
        SelectedStatus = string.IsNullOrWhiteSpace(disease.Status) ? "Active" : disease.Status;
        StartDate = disease.StartDate == default ? DateTime.Today : disease.StartDate;
        HasRecoveryDate = disease.RecoveryDate.HasValue;
        RecoveryDate = disease.RecoveryDate;
    }

    private async Task SaveAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var disease = new Disease
            {
                Id = _diseaseId ?? Guid.Empty,
                DogId = _dogId,
                Name = Diagnosis.Trim(),
                Symptoms = string.IsNullOrWhiteSpace(Symptoms) ? null : Symptoms.Trim(),
                StartDate = StartDate.Date,
                RecoveryDate = HasRecoveryDate ? RecoveryDate?.Date : null,
                Status = SelectedStatus
            };

            (bool success, string error) = _diseaseId == null
                ? await _diseaseService.AddAsync(disease)
                : await _diseaseService.UpdateAsync(disease);

            if (!success)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    string.IsNullOrWhiteSpace(error) ? "Could not save disease." : error,
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
