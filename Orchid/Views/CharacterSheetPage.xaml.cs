namespace Orchid.Views;
using Orchid.ViewModels;
public partial class CharacterSheetPage : ContentPage
{
    private CharacterSheetViewModel _viewModel;
    public CharacterSheetPage(CharacterSheetViewModel vm)
    {
        CharacterSheetViewModel _viewModel = vm;
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        CharacterSheetViewModel _vm = (BindingContext as CharacterSheetViewModel);
        await _vm.InitializeAsync();// you can have some additional logic to cache the result` 
    }
    protected override async void OnDisappearing()
    {
        ((App)Application.Current).ReviewUser = ((App)Application.Current).ReviewUserDefault;
    }
}