using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_skillsView : ContentPage
{
    public AA_skillsView(AA_skillsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        AA_skillsViewModel _vm = (BindingContext as AA_skillsViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}