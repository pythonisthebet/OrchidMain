using Orchid.ViewModels;

namespace Orchid.Views;

public partial class Ch_ListView : ContentPage
{
    public Ch_ListView(Ch_ListViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}