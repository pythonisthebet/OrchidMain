using Orchid.ViewModels;
namespace Orchid.Views;

public partial class UserListAView : ContentPage
{
    public UserListAView(UserListAViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    //Calls the InitilizeAsync function on page appearence to load data
    protected override async void OnAppearing()
    {
        UserListAViewModel _vm = (BindingContext as UserListAViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }

    //Eraser user data after admin use on exiting page 
    protected override async void OnDisappearing()
    {
        ((App)Application.Current).ReviewUser = ((App)Application.Current).ReviewUserDefault;
    }
}