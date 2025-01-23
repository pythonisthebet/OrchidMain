using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_classView : ContentPage
{
    private readonly AA_classViewModel _vm;
    public AA_classView(AA_classViewModel vm)
    {
        object _vm = vm;
        this.BindingContext = vm;
        InitializeComponent();
    }


    protected override async void OnAppearing()
    {
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}