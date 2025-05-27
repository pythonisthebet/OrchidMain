using Orchid.ViewModels;
namespace Orchid.Views;

public partial class AppealListAView : ContentPage
{
    public AppealListAView(AppealListAViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        AppealListAViewModel _vm = (BindingContext as AppealListAViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}