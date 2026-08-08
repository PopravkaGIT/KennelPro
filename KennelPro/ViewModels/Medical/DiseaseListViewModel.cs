using System.Collections.ObjectModel;
using System.Windows.Input;
using KennelPro.Pages.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class DiseaseListViewModel : BaseViewModel, IQueryAttributable
{
    private readonly DiseaseService _diseaseService;

    private Guid _dogId;

    public DiseaseListViewModel(DiseaseService diseaseService)
    {
        _diseaseService = diseaseService;
        Items = new ObservableCollection<DiseaseListItem>();

        AddCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(DiseaseEditPage)}?dogId={_dogId}"));

        EditCommand = new Command<DiseaseListItem>(async item =>
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(DiseaseEditPage)}?dogId={_dogId}&diseaseId={item.Id}");
        });

        DeleteCommand = new Command<DiseaseListItem>(async item =>
        {
            if (item == null)
                return;

            await DeleteAsync(item);
        });

        RefreshCommand = new Command(async () => await LoadAsync());
        BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ObservableCollection<DiseaseListItem> Items { get; }

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

            var items = await _diseaseService.GetForDogAsync(_dogId);
            foreach (var item in items.OrderByDescending(d => d.StartDate))
                Items.Add(DiseaseListItem.FromDisease(item));
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task DeleteAsync(DiseaseListItem item)
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Delete Disease",
            $"Delete {item.Diagnosis}? This cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirmed)
            return;

        bool deleted = await _diseaseService.DeleteAsync(item.Id);
        if (!deleted)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Could not delete this disease.",
                "OK");
            return;
        }

        await LoadAsync();
    }
}
