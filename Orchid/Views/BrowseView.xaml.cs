using Orchid.ViewModels;

namespace Orchid.Views;

public partial class BrowseView : ContentPage
{
    public BrowseView(BrowseViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}