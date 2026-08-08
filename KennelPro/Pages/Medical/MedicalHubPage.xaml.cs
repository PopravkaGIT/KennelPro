using KennelPro.ViewModels.Medical;

namespace KennelPro.Pages.Medical;

public partial class MedicalHubPage : ContentPage, IQueryAttributable
{
    private readonly MedicalHubViewModel _viewModel;

    public MedicalHubPage(MedicalHubViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _viewModel.ApplyQueryAttributes(query);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}
