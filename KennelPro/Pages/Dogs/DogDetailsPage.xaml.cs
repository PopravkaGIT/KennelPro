using KennelPro.ViewModels.Dogs;

namespace KennelPro.Pages.Dogs;

public partial class DogDetailsPage : ContentPage, IQueryAttributable
{
    private readonly DogDetailsViewModel _viewModel;

    public DogDetailsPage(DogDetailsViewModel viewModel)
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
