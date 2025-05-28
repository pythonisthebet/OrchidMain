using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_featsView : ContentPage
{
    public AA_featsView(AA_featsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    //Calls the InitilizeAsync function on page appearence to load data
    protected override async void OnAppearing()
    {
        AA_featsViewModel _vm = (BindingContext as AA_featsViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}