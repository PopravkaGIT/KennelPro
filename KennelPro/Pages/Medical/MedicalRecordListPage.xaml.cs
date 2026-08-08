using KennelPro.ViewModels.Medical;

namespace KennelPro.Pages.Medical;

public partial class MedicalRecordListPage : ContentPage, IQueryAttributable
{
    private readonly MedicalRecordListViewModel _viewModel;

    public MedicalRecordListPage(MedicalRecordListViewModel viewModel)
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
