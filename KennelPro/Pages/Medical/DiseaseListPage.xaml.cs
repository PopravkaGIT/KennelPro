using KennelPro.ViewModels.Medical;
namespace KennelPro.Pages.Medical;
public partial class DiseaseListPage : ContentPage, IQueryAttributable
{ private readonly DiseaseListViewModel _viewModel; public DiseaseListPage(DiseaseListViewModel viewModel) { InitializeComponent(); _viewModel = viewModel; BindingContext = viewModel; } public void ApplyQueryAttributes(IDictionary<string, object> query) => _viewModel.ApplyQueryAttributes(query); protected override async void OnAppearing() { base.OnAppearing(); await _viewModel.LoadAsync(); } }
