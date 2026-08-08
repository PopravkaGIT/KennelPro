using System.Windows.Input;
using KennelPro.Services.Authentication;

namespace KennelPro.ViewModels.Authentication;

public class RegisterViewModel : BaseViewModel
{
    private readonly AuthenticationService _authenticationService;

    public RegisterViewModel(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;

        RegisterCommand = new Command(async () => await RegisterAsync());
        GoToLoginCommand = new Command(async () =>
            await Shell.Current.GoToAsync(".."));
    }

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
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

    private string _confirmPassword = string.Empty;
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => SetProperty(ref _confirmPassword, value);
    }

    private string _kennelName = string.Empty;
    public string KennelName
    {
        get => _kennelName;
        set => SetProperty(ref _kennelName, value);
    }

    public ICommand RegisterCommand { get; }
    public ICommand GoToLoginCommand { get; }

    private async Task RegisterAsync()
    {
        if (Password != ConfirmPassword)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Passwords do not match.",
                "OK");

            return;
        }

        var user = await _authenticationService.RegisterAsync(
            Name,
            Email,
            Password,
            KennelName);

        if (user == null)
        {
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Registration failed. Check your details or use a different email.",
                "OK");

            return;
        }

        await Shell.Current.GoToAsync(nameof(MainPage), true);
    }
}
