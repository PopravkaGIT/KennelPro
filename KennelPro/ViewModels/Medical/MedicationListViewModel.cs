using System.Collections.ObjectModel;
using System.Windows.Input;
using KennelPro.Pages.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class MedicationListViewModel : BaseViewModel, IQueryAttributable
{
    private readonly MedicationService _medicationService;

    private Guid _dogId;

    public MedicationListViewModel(MedicationService medicationService)
    {
        _medicationService = medicationService;
        Items = new ObservableCollection<MedicationListItem>();

        AddCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(MedicationEditPage)}?dogId={_dogId}"));

        EditCommand = new Command<MedicationListItem>(async item =>
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(MedicationEditPage)}?dogId={_dogId}&medicationId={item.Id}");
        });

        DeleteCommand = new Command<MedicationListItem>(async item =>
        {
            if (item == null)
                return;

            await DeleteAsync(item);
        });

        RefreshCommand = new Command(async () => await LoadAsync());
        BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ObservableCollection<MedicationListItem> Items { get; }

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

            var items = await _medicationService.GetForDogAsync(_dogId);
            foreach (var item in items.OrderByDescending(m => m.StartDate))
                Items.Add(MedicationListItem.FromMedication(item));
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task DeleteAsync(MedicationListItem item)
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Delete Medication",
            $"Delete {item.Name}? This cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirmed)
            return;

        bool deleted = await _medicationService.DeleteAsync(item.Id);
        if (!deleted)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Could not delete this medication.",
                "OK");
            return;
        }

        await LoadAsync();
    }
}
