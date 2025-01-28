using Orchid.ViewModels;

namespace Orchid.Views;

public partial class Ch_ListView : ContentPage
{
    private readonly Ch_ListViewModel _vm;

    public Ch_ListView(Ch_ListViewModel vm)
    {
        Ch_ListViewModel _vm = vm;
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        Ch_ListViewModel _vm = (BindingContext as Ch_ListViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}