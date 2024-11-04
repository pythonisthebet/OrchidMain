using Orchid.ViewModels;

namespace Orchid.Views;

public partial class ForumsView : ContentPage
{
    public ForumsView(ForumsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}