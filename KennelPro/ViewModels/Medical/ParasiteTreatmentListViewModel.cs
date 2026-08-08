using System.Collections.ObjectModel;
using System.Windows.Input;
using KennelPro.Pages.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class ParasiteTreatmentListViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ParasiteService _parasiteService;

    private Guid _dogId;

    public ParasiteTreatmentListViewModel(ParasiteService parasiteService)
    {
        _parasiteService = parasiteService;
        Items = new ObservableCollection<ParasiteTreatmentListItem>();

        AddCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(ParasiteTreatmentEditPage)}?dogId={_dogId}"));

        EditCommand = new Command<ParasiteTreatmentListItem>(async item =>
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(ParasiteTreatmentEditPage)}?dogId={_dogId}&treatmentId={item.Id}");
        });

        DeleteCommand = new Command<ParasiteTreatmentListItem>(async item =>
        {
            if (item == null)
                return;

            await DeleteAsync(item);
        });

        RefreshCommand = new Command(async () => await LoadAsync());
        BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ObservableCollection<ParasiteTreatmentListItem> Items { get; }

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

            var items = await _parasiteService.GetForDogAsync(_dogId);
            foreach (var item in items.OrderByDescending(t => t.Date))
                Items.Add(ParasiteTreatmentListItem.FromTreatment(item));
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task DeleteAsync(ParasiteTreatmentListItem item)
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Delete Treatment",
            $"Delete {item.ProductName}? This cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirmed)
            return;

        bool deleted = await _parasiteService.DeleteAsync(item.Id);
        if (!deleted)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Could not delete this treatment.",
                "OK");
            return;
        }

        await LoadAsync();
    }
}
