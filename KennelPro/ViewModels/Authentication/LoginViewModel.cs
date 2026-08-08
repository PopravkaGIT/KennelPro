using System.Windows.Input;
using KennelPro.Pages.Authentication;
using KennelPro.Services.Authentication;

namespace KennelPro.ViewModels.Authentication;

public class LoginViewModel : BaseViewModel
{
    private readonly AuthenticationService _authenticationService;

    public LoginViewModel(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;

        LoginCommand = new Command(async () => await LoginAsync());
        GoToRegisterCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(RegisterPage)));
    }

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public ICommand LoginCommand { get; }
    public ICommand GoToRegisterCommand { get; }

    private async Task LoginAsync()
    {
        var user = await _authenticationService.LoginAsync(
            Email,
            Password);

        if (user == null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Invalid email or password.",
                "OK");

            return;
        }

        await Shell.Current.GoToAsync(nameof(MainPage), true);
    }
}
