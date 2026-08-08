using KennelPro.ViewModels.Medical;

namespace KennelPro.Pages.Medical;

public partial class VaccinationListPage : ContentPage, IQueryAttributable
{
    private readonly VaccinationListViewModel _viewModel;

    public VaccinationListPage(VaccinationListViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) => _viewModel.ApplyQueryAttributes(query);

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}
