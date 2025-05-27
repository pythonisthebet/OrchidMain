using Orchid.ViewModels;
namespace Orchid.Views;

public partial class UserListAView : ContentPage
{
    public UserListAView(UserListAViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        UserListAViewModel _vm = (BindingContext as UserListAViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}