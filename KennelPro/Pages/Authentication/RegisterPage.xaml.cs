using KennelPro.ViewModels.Authentication;

namespace KennelPro.Pages.Authentication;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
