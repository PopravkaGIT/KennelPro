using KennelPro.Helpers;
using KennelPro.Pages.Authentication;
using KennelPro.Pages.Dogs;
using KennelPro.Pages.Medical;
using KennelPro.Pages.Reproduction;

namespace KennelPro;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(DogsPage), typeof(DogsPage));
        Routing.RegisterRoute(nameof(DogEditPage), typeof(DogEditPage));
        Routing.RegisterRoute(nameof(DogDetailsPage), typeof(DogDetailsPage));
        Routing.RegisterRoute(nameof(MedicalHubPage), typeof(MedicalHubPage));
        Routing.RegisterRoute(nameof(MedicalRecordListPage), typeof(MedicalRecordListPage));
        Routing.RegisterRoute(nameof(MedicalRecordEditPage), typeof(MedicalRecordEditPage));
        Routing.RegisterRoute(nameof(VaccinationListPage), typeof(VaccinationListPage));
        Routing.RegisterRoute(nameof(VaccinationEditPage), typeof(VaccinationEditPage));
           
        Routing.RegisterRoute(nameof(ParasiteTreatmentListPage), typeof(ParasiteTreatmentListPage));
        Routing.RegisterRoute(nameof(ParasiteTreatmentEditPage), typeof(ParasiteTreatmentEditPage));
        Routing.RegisterRoute(nameof(MedicationListPage), typeof(MedicationListPage));
        Routing.RegisterRoute(nameof(MedicationEditPage), typeof(MedicationEditPage));
        Routing.RegisterRoute(nameof(DiseaseListPage), typeof(DiseaseListPage));
        Routing.RegisterRoute(nameof(DiseaseEditPage), typeof(DiseaseEditPage));
        Routing.RegisterRoute(nameof(ReproductionPage), typeof(ReproductionPage));
        Routing.RegisterRoute(nameof(ReproductionCrudPage), typeof(ReproductionCrudPage));

        Loaded += OnShellLoaded;
    }

    private async void OnShellLoaded(object? sender, EventArgs e)
    {
        Loaded -= OnShellLoaded;

        if (SessionManager.IsLoggedIn())
            await GoToAsync(nameof(MainPage), true);
    }
}
