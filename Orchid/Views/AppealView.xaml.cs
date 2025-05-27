using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AppealView : ContentPage
{
    public AppealView(AppealViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        AppealViewModel _vm = (BindingContext as AppealViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }

    protected override async void OnDisappearing()
    {
        ((App)Application.Current).ReviewUser = ((App)Application.Current).ReviewUserDefault;
    }
}