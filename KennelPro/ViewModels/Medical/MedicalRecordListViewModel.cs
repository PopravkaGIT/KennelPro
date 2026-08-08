using System.Collections.ObjectModel;
using System.Windows.Input;
using KennelPro.Pages.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class MedicalRecordListViewModel : BaseViewModel, IQueryAttributable
{
    private readonly MedicalService _medicalService;

    private Guid _dogId;

    public MedicalRecordListViewModel(MedicalService medicalService)
    {
        _medicalService = medicalService;
        Items = new ObservableCollection<MedicalRecordListItem>();

        AddCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(MedicalRecordEditPage)}?dogId={_dogId}"));

        EditCommand = new Command<MedicalRecordListItem>(async item =>
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(MedicalRecordEditPage)}?dogId={_dogId}&recordId={item.Id}");
        });

        DeleteCommand = new Command<MedicalRecordListItem>(async item =>
        {
            if (item == null)
                return;

            await DeleteAsync(item);
        });

        RefreshCommand = new Command(async () => await LoadAsync());
        BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ObservableCollection<MedicalRecordListItem> Items { get; }

    public ICommand AddCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand BackCommand { get; }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("dogId", out var value))
        {
            var idText = value?.ToString();
            if (!string.IsNullOrWhiteSpace(idText) && Guid.TryParse(idText, out var id))
                _dogId = id;
        }
    }

    public async Task LoadAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Items.Clear();

            var records = await _medicalService.GetForDogAsync(_dogId);
            foreach (var record in records.OrderByDescending(r => r.Date))
                Items.Add(MedicalRecordListItem.FromRecord(record));
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task DeleteAsync(MedicalRecordListItem item)
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Delete Record",
            "Delete this medical record? This cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirmed)
            return;

        bool deleted = await _medicalService.DeleteAsync(item.Id);
        if (!deleted)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Could not delete this record.",
                "OK");
            return;
        }

        await LoadAsync();
    }
}
