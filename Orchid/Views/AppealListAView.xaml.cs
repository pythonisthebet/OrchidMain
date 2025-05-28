using Orchid.ViewModels;
namespace Orchid.Views;

public partial class AppealListAView : ContentPage
{
    public AppealListAView(AppealListAViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    //Calls the InitilizeAsync function on page appearence to load data
    protected override async void OnAppearing()
    {
        AppealListAViewModel _vm = (BindingContext as AppealListAViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }

    //Eraser user data after admin use on exiting page 
    protected override async void OnDisappearing()
    {
        ((App)Application.Current).ReviewUser = ((App)Application.Current).ReviewUserDefault;
    }
}