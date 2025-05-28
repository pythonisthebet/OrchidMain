using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_statsView : ContentPage
{
    public AA_statsView(AA_statsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    //Calls the InitilizeAsync function on page appearence to load data
    protected override async void OnAppearing()
    {
        AA_statsViewModel _vm = (BindingContext as AA_statsViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}
