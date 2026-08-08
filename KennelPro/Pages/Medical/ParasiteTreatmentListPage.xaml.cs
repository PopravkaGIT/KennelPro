using KennelPro.ViewModels.Medical;
namespace KennelPro.Pages.Medical;
public partial class ParasiteTreatmentListPage : ContentPage, IQueryAttributable
{ private readonly ParasiteTreatmentListViewModel _viewModel; public ParasiteTreatmentListPage(ParasiteTreatmentListViewModel viewModel) { InitializeComponent(); _viewModel = viewModel; BindingContext = viewModel; } public void ApplyQueryAttributes(IDictionary<string, object> query) => _viewModel.ApplyQueryAttributes(query); protected override async void OnAppearing() { base.OnAppearing(); await _viewModel.LoadAsync(); } }
