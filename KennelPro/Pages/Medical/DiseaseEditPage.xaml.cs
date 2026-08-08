using KennelPro.ViewModels.Medical;
namespace KennelPro.Pages.Medical;
public partial class DiseaseEditPage : ContentPage, IQueryAttributable
{ private readonly DiseaseEditViewModel _viewModel; public DiseaseEditPage(DiseaseEditViewModel viewModel) { InitializeComponent(); _viewModel = viewModel; BindingContext = viewModel; } public void ApplyQueryAttributes(IDictionary<string, object> query) => _viewModel.ApplyQueryAttributes(query); protected override async void OnAppearing() { base.OnAppearing(); await _viewModel.InitializeAsync(); } }
