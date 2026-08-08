using KennelPro.Pages.Dogs;
using KennelPro.Pages.Reproduction;
using KennelPro.Services.Authentication;

namespace KennelPro;

public partial class MainPage : ContentPage
{
    private readonly AuthenticationService _authenticationService;

    public MainPage(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_authenticationService.IsLoggedIn())
        {
            await Shell.Current.GoToAsync("//LoginPage");
            return;
        }

        var user = await _authenticationService.GetCurrentUserAsync();

        if (user == null)
        {
            _authenticationService.Logout();
            await Shell.Current.GoToAsync("//LoginPage");
            return;
        }

        WelcomeLabel.Text = $"Welcome, {user.Name}!";
        KennelLabel.Text = user.Kennel?.Name ?? string.Empty;
    }

    private async void OnDogsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DogsPage));
    }
    private async void OnReproductionClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ReproductionPage));
    }

    private async void OnLogoutClicked(object? sender, EventArgs e)
    {
        _authenticationService.Logout();
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
