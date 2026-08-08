using System.Collections.ObjectModel;
using System.Windows.Input;
using KennelPro.Pages.Dogs;
using KennelPro.Services.Dogs;

namespace KennelPro.ViewModels.Dogs;

public class DogListViewModel : BaseViewModel
{
    private readonly DogService _dogService;

    private List<DogListItem> _allItems = new();

    public DogListViewModel(DogService dogService)
    {
        _dogService = dogService;

        Dogs = new ObservableCollection<DogListItem>();

        AddDogCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(DogEditPage)));

        RefreshCommand = new Command(async () => await LoadDogsAsync());
        SelectDogCommand = new Command<DogListItem>(async item =>
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(DogDetailsPage)}?dogId={item.Id}");
        });

        EditDogCommand = new Command<DogListItem>(async item =>
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(DogEditPage)}?dogId={item.Id}");
        });

        DeleteDogCommand = new Command<DogListItem>(async item =>
        {
            if (item == null)
                return;

            await DeleteDogAsync(item);
        });
    }

    public ObservableCollection<DogListItem> Dogs { get; }

    public ICommand AddDogCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand SelectDogCommand { get; }
    public ICommand EditDogCommand { get; }
    public ICommand DeleteDogCommand { get; }

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
                ApplySearchFilter();
        }
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public async Task LoadDogsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var dogs = await _dogService.GetDogsForCurrentKennelAsync();
            _allItems = dogs.Select(DogListItem.FromDog).ToList();
            ApplySearchFilter();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ApplySearchFilter()
    {
        Dogs.Clear();

        var query = _searchText.Trim();
        IEnumerable<DogListItem> filtered = string.IsNullOrWhiteSpace(query)
            ? _allItems
            : _allItems.Where(d =>
                d.Name.Contains(query, StringComparison.OrdinalIgnoreCase));

        foreach (var item in filtered)
            Dogs.Add(item);
    }

    private async Task DeleteDogAsync(DogListItem item)
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Delete Dog",
            $"Delete {item.Name}? This cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirmed)
            return;

        bool deleted = await _dogService.DeleteDogAsync(item.Id);

        if (!deleted)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Could not delete this dog.",
                "OK");
            return;
        }

        await LoadDogsAsync();
    }
}
