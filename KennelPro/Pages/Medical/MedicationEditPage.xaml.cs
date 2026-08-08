using KennelPro.ViewModels.Medical;
namespace KennelPro.Pages.Medical;
public partial class MedicationEditPage : ContentPage, IQueryAttributable
{ private readonly MedicationEditViewModel _viewModel; public MedicationEditPage(MedicationEditViewModel viewModel) { InitializeComponent(); _viewModel = viewModel; BindingContext = viewModel; } public void ApplyQueryAttributes(IDictionary<string, object> query) => _viewModel.ApplyQueryAttributes(query); protected override async void OnAppearing() { base.OnAppearing(); await _viewModel.InitializeAsync(); } }
