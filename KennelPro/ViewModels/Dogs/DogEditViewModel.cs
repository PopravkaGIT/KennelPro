using System.Collections.ObjectModel;
using System.Windows.Input;
using KennelPro.Models.Dogs;
using KennelPro.Models.Enums;
using KennelPro.Services.Dogs;

namespace KennelPro.ViewModels.Dogs;

public class DogEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly DogService _dogService;
    private readonly BreedService _breedService;

    private Guid? _dogId;

    public DogEditViewModel(DogService dogService, BreedService breedService)
    {
        _dogService = dogService;
        _breedService = breedService;

        Breeds = new ObservableCollection<Breed>();
        Genders = new ObservableCollection<string> { "Male", "Female" };

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ObservableCollection<Breed> Breeds { get; }
    public ObservableCollection<string> Genders { get; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    private string _pageTitle = "Add Dog";
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

    private Breed? _selectedBreed;
    public Breed? SelectedBreed
    {
        get => _selectedBreed;
        set => SetProperty(ref _selectedBreed, value);
    }

    private string _selectedGender = "Male";
    public string SelectedGender
    {
        get => _selectedGender;
        set => SetProperty(ref _selectedGender, value);
    }

    private DateTime _birthDate = DateTime.Today.AddYears(-1);
    public DateTime BirthDate
    {
        get => _birthDate;
        set => SetProperty(ref _birthDate, value);
    }

    private string _chipNumber = string.Empty;
    public string ChipNumber
    {
        get => _chipNumber;
        set => SetProperty(ref _chipNumber, value);
    }

    private string _pedigreeNumber = string.Empty;
    public string PedigreeNumber
    {
        get => _pedigreeNumber;
        set => SetProperty(ref _pedigreeNumber, value);
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
        if (query.TryGetValue("dogId", out var value))
        {
            var idText = value?.ToString();
            if (!string.IsNullOrWhiteSpace(idText) && Guid.TryParse(idText, out var id))
                _dogId = id;
        }
    }

    public async Task InitializeAsync()
    {
        await LoadBreedsAsync();

        if (_dogId == null)
        {
            PageTitle = "Add Dog";
            return;
        }

        PageTitle = "Edit Dog";

        var dog = await _dogService.GetDogForCurrentKennelAsync(_dogId.Value);
        if (dog == null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Dog not found.",
                "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        Name = dog.Name;
        SelectedBreed = Breeds.FirstOrDefault(b => b.Id == dog.BreedId);
        SelectedGender = dog.Gender == Gender.Male ? "Male" : "Female";
        BirthDate = dog.BirthDate;
        ChipNumber = dog.ChipNumber ?? string.Empty;
        PedigreeNumber = dog.PedigreeNumber ?? string.Empty;
        Notes = dog.Notes ?? string.Empty;
    }

    private async Task LoadBreedsAsync()
    {
        Breeds.Clear();
        var breeds = await _breedService.GetAllBreedsAsync();
        foreach (var breed in breeds.OrderBy(b => b.Name))
            Breeds.Add(breed);
    }

    private async Task SaveAsync()
    {
        if (IsBusy)
            return;

        if (SelectedBreed == null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Please select a breed.",
                "OK");
            return;
        }

        try
        {
            IsBusy = true;

            var dog = new Dog
            {
                Id = _dogId ?? Guid.Empty,
                Name = Name.Trim(),
                BreedId = SelectedBreed.Id,
                Gender = SelectedGender == "Female" ? Gender.Female : Gender.Male,
                BirthDate = BirthDate.Date,
                ChipNumber = string.IsNullOrWhiteSpace(ChipNumber) ? null : ChipNumber.Trim(),
                PedigreeNumber = string.IsNullOrWhiteSpace(PedigreeNumber) ? null : PedigreeNumber.Trim(),
                Notes = string.IsNullOrWhiteSpace(Notes) ? null : Notes.Trim()
            };

            (bool success, string error) = _dogId == null
                ? await _dogService.AddDogAsync(dog)
                : await _dogService.UpdateDogAsync(dog);

            if (!success)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    string.IsNullOrWhiteSpace(error) ? "Could not save dog." : error,
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
