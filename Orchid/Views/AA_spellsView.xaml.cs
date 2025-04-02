using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_spellsView : ContentPage
{
    public AA_spellsView(AA_spellsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        AA_spellsViewModel _vm = (BindingContext as AA_spellsViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}