using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_subclassView : ContentPage
{
    public AA_subclassView(AA_subclassViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    //protected override async void OnAppearing()
    //{
    //    AA_subclassViewModel _vm = (BindingContext as AA_subclassViewModel);
    //    await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    //}
}