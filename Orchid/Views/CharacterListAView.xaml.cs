using Orchid.ViewModels;

namespace Orchid.Views;

public partial class CharacterListAView : ContentPage
{
    public CharacterListAView(CharacterListAViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        CharacterListAViewModel _vm = (BindingContext as CharacterListAViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }

    protected override async void OnDisappearing()
    {
        ((App)Application.Current).ReviewUser = ((App)Application.Current).ReviewUserDefault;
    }
}