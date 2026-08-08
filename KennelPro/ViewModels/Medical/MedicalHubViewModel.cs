using System.Windows.Input;
using KennelPro.Pages.Medical;
using KennelPro.Services.Dogs;

namespace KennelPro.ViewModels.Medical;

public class MedicalHubViewModel : BaseViewModel, IQueryAttributable
{
    private readonly DogService _dogService;

    private Guid _dogId;

    public MedicalHubViewModel(DogService dogService)
    {
        _dogService = dogService;

        OpenRecordsCommand = new Command(async () =>
            await NavigateAsync(nameof(MedicalRecordListPage)));

        OpenVaccinationsCommand = new Command(async () =>
            await NavigateAsync(nameof(VaccinationListPage)));

        OpenParasitesCommand = new Command(async () =>
            await NavigateAsync(nameof(ParasiteTreatmentListPage)));

        OpenMedicationsCommand = new Command(async () =>
            await NavigateAsync(nameof(MedicationListPage)));

        OpenDiseasesCommand = new Command(async () =>
            await NavigateAsync(nameof(DiseaseListPage)));

        BackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public ICommand OpenRecordsCommand { get; }
    public ICommand OpenVaccinationsCommand { get; }
    public ICommand OpenParasitesCommand { get; }
    public ICommand OpenMedicationsCommand { get; }
    public ICommand OpenDiseasesCommand { get; }
    public ICommand BackCommand { get; }

    private string _dogName = string.Empty;
    public string DogName
    {
        get => _dogName;
        set => SetProperty(ref _dogName, value);
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

        DogName = dog.Name;
    }

    private async Task NavigateAsync(string pageName)
    {
        await Shell.Current.GoToAsync($"{pageName}?dogId={_dogId}");
    }
}
