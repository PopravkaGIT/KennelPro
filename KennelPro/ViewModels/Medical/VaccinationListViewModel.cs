using System.Collections.ObjectModel;
using System.Windows.Input;
using KennelPro.Pages.Medical;
using KennelPro.Services.Medical;

namespace KennelPro.ViewModels.Medical;

public class VaccinationListViewModel : BaseViewModel, IQueryAttributable
{
    private readonly VaccinationService _vaccinationService;

    private Guid _dogId;

    public VaccinationListViewModel(VaccinationService vaccinationService)
    {
        _vaccinationService = vaccinationService;
        Items = new ObservableCollection<VaccinationListItem>();

        AddCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(VaccinationEditPage)}?dogId={_dogId}"));

        EditCommand = new Command<VaccinationListItem>(async item =>
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(VaccinationEditPage)}?dogId={_dogId}&vaccinationId={item.Id}");
        });

        DeleteCommand = new Command<VaccinationListItem>(async item =>
        {
            if (item == null)
                return;

            await DeleteAsync(item);
        });

        RefreshCommand = new Command(async () => await LoadAsync());
        BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ObservableCollection<VaccinationListItem> Items { get; }

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

            var items = await _vaccinationService.GetForDogAsync(_dogId);
            foreach (var item in items.OrderByDescending(v => v.VaccinationDate))
                Items.Add(VaccinationListItem.FromVaccination(item));
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task DeleteAsync(VaccinationListItem item)
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Delete Vaccination",
            $"Delete {item.Name}? This cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirmed)
            return;

        bool deleted = await _vaccinationService.DeleteAsync(item.Id);
        if (!deleted)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Could not delete this vaccination.",
                "OK");
            return;
        }

        await LoadAsync();
    }
}
