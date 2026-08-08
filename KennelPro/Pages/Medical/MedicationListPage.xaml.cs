using KennelPro.ViewModels.Medical;
namespace KennelPro.Pages.Medical;
public partial class MedicationListPage : ContentPage, IQueryAttributable
{ private readonly MedicationListViewModel _viewModel; public MedicationListPage(MedicationListViewModel viewModel) { InitializeComponent(); _viewModel = viewModel; BindingContext = viewModel; } public void ApplyQueryAttributes(IDictionary<string, object> query) => _viewModel.ApplyQueryAttributes(query); protected override async void OnAppearing() { base.OnAppearing(); await _viewModel.LoadAsync(); } }
