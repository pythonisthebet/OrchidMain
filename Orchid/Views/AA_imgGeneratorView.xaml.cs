using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_imgGeneratorView : ContentPage
{
    public AA_imgGeneratorView(AA_imgGeneratorViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        AA_imgGeneratorViewModel _vm = (BindingContext as AA_imgGeneratorViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }

}