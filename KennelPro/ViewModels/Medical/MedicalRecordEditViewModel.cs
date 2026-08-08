using System.Windows.Input;
using KennelPro.Models.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class MedicalRecordEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly MedicalService _medicalService;

    private Guid _dogId;
    private Guid? _recordId;

    public MedicalRecordEditViewModel(MedicalService medicalService)
    {
        _medicalService = medicalService;

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    private string _pageTitle = "Add Record";
    public string PageTitle
    {
        get => _pageTitle;
        set => SetProperty(ref _pageTitle, value);
    }

    private DateTime _date = DateTime.Today;
    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
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

        if (query.TryGetValue("recordId", out var recordValue))
        {
            var recordText = recordValue?.ToString();
            if (!string.IsNullOrWhiteSpace(recordText) && Guid.TryParse(recordText, out var recordId))
                _recordId = recordId;
        }
    }

    public async Task InitializeAsync()
    {
        if (_recordId == null)
        {
            PageTitle = "Add Record";
            return;
        }

        PageTitle = "Edit Record";

        var record = await _medicalService.GetByIdAsync(_recordId.Value);
        if (record == null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Record not found.",
                "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        Date = record.Date;
        Notes = record.Notes ?? string.Empty;
    }

    private async Task SaveAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var record = new MedicalRecord
            {
                Id = _recordId ?? Guid.Empty,
                DogId = _dogId,
                Date = Date.Date,
                Notes = string.IsNullOrWhiteSpace(Notes) ? null : Notes.Trim()
            };

            (bool success, string error) = _recordId == null
                ? await _medicalService.AddAsync(record)
                : await _medicalService.UpdateAsync(record);

            if (!success)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    string.IsNullOrWhiteSpace(error) ? "Could not save record." : error,
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
