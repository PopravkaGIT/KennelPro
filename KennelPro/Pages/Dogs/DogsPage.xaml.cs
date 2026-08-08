using KennelPro.ViewModels.Dogs;

namespace KennelPro.Pages.Dogs;

public partial class DogsPage : ContentPage
{
    private readonly DogListViewModel _viewModel;

    public DogsPage(DogListViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDogsAsync();
    }
}
