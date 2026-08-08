using System.Windows.Input;
using KennelPro.Helpers;
using KennelPro.Models.Enums;
using KennelPro.Pages.Dogs;
using KennelPro.Pages.Medical;
using KennelPro.Pages.Reproduction;
using KennelPro.Services.Dogs;

namespace KennelPro.ViewModels.Dogs;

public class DogDetailsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly DogService _dogService;

    private Guid _dogId;

    public DogDetailsViewModel(DogService dogService)
    {
        _dogService = dogService;

        EditCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(DogEditPage)}?dogId={_dogId}"));

        DeleteCommand = new Command(async () => await DeleteAsync());
        MedicalCardCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(MedicalHubPage)}?dogId={_dogId}"));
        ReproductionCommand = new Command(async () => await Shell.Current.GoToAsync($"{nameof(ReproductionPage)}?dogId={_dogId}"));
        BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand MedicalCardCommand { get; }
    public ICommand ReproductionCommand { get; }
    public ICommand BackCommand { get; }

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _breedName = string.Empty;
    public string BreedName
    {
        get => _breedName;
        set => SetProperty(ref _breedName, value);
    }

    private string _genderText = string.Empty;
    public string GenderText
    {
        get => _genderText;
        set => SetProperty(ref _genderText, value);
    }

    private string _birthDateText = string.Empty;
    public string BirthDateText
    {
        get => _birthDateText;
        set => SetProperty(ref _birthDateText, value);
    }

    private string _chipNumber = "-";
    public string ChipNumber
    {
        get => _chipNumber;
        set => SetProperty(ref _chipNumber, value);
    }

    private string _pedigreeNumber = "-";
    public string PedigreeNumber
    {
        get => _pedigreeNumber;
        set => SetProperty(ref _pedigreeNumber, value);
    }

    private string _notes = "-";
    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
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
        var dog = await _dogService.GetDogForCurrentKennelAsync(_dogId);
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
        BreedName = dog.Breed?.Name ?? "-";
        GenderText = dog.Gender == Gender.Male ? "Male" : "Female";
        BirthDateText = DateHelper.FormatDate(dog.BirthDate);
        ChipNumber = string.IsNullOrWhiteSpace(dog.ChipNumber) ? "-" : dog.ChipNumber;
        PedigreeNumber = string.IsNullOrWhiteSpace(dog.PedigreeNumber) ? "-" : dog.PedigreeNumber;
        Notes = string.IsNullOrWhiteSpace(dog.Notes) ? "-" : dog.Notes;
    }

    private async Task DeleteAsync()
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Delete Dog",
            $"Delete {Name}? This cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirmed)
            return;

        bool deleted = await _dogService.DeleteDogAsync(_dogId);
        if (!deleted)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Could not delete this dog.",
                "OK");
            return;
        }

        await Shell.Current.GoToAsync("..");
    }
}
