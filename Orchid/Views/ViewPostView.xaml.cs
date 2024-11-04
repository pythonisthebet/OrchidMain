using Orchid.ViewModels;

namespace Orchid.Views;

public partial class ViewPostView : ContentPage
{
    public ViewPostView(ViewPostViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}