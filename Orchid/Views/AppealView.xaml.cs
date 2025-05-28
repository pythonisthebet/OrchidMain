using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AppealView : ContentPage
{
    public AppealView(AppealViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    //Calls the InitilizeAsync function on page appearence to load data
    protected override async void OnAppearing()
    {
        AppealViewModel _vm = (BindingContext as AppealViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }

    //Eraser user data after admin use on exiting page 
    protected override async void OnDisappearing()
    {
        ((App)Application.Current).ReviewUser = ((App)Application.Current).ReviewUserDefault;
    }
}