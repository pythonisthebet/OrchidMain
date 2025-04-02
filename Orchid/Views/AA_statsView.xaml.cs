using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_statsView : ContentPage
{
    public AA_statsView(AA_statsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    //protected override async void OnAppearing()
    //{
    //    AA_statsViewModel _vm = (BindingContext as AA_statsViewModel);
    //    await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    //}
}
