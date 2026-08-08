using KennelPro.ViewModels.Dogs;

namespace KennelPro.Pages.Dogs;

public partial class DogEditPage : ContentPage, IQueryAttributable
{
    private readonly DogEditViewModel _viewModel;

    public DogEditPage(DogEditViewModel viewModel)
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
        await _viewModel.InitializeAsync();
    }
}
