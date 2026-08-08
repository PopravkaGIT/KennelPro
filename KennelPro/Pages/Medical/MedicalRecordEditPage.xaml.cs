using KennelPro.ViewModels.Medical;

namespace KennelPro.Pages.Medical;

public partial class MedicalRecordEditPage : ContentPage, IQueryAttributable
{
    private readonly MedicalRecordEditViewModel _viewModel;

    public MedicalRecordEditPage(MedicalRecordEditViewModel viewModel)
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
