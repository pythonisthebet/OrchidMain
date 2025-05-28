using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_equipmentView : ContentPage
{
    public AA_equipmentView(AA_equipmentViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    //Calls the InitilizeAsync function on page appearence to load data
    protected override async void OnAppearing()
    {
        AA_equipmentViewModel _vm = (BindingContext as AA_equipmentViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}