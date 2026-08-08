using KennelPro.ViewModels.Reproduction;
namespace KennelPro.Pages.Reproduction;
public partial class ReproductionPage:ContentPage,IQueryAttributable { readonly ReproductionViewModel _vm; public ReproductionPage(ReproductionViewModel vm){InitializeComponent();_vm=vm;BindingContext=vm;} public void ApplyQueryAttributes(IDictionary<string,object> q)=>_vm.ApplyQueryAttributes(q); protected override async void OnAppearing(){base.OnAppearing();await _vm.LoadAsync();} }
