using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_classView : ContentPage
{
    private readonly AA_classViewModel _vm;
    public AA_classView(AA_classViewModel vm)
    {
        AA_classViewModel _vm = vm;
        this.BindingContext = vm;
        InitializeComponent();
    }


    protected override async void OnAppearing()
    {
        AA_classViewModel _vm = (BindingContext as AA_classViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }

    //protected override async void OnBackButtonPressed()
    //{
    //    AA_classViewModel _vm = (BindingContext as AA_classViewModel);
    //    await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    //}
}