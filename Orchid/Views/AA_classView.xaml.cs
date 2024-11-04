using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_classView : ContentPage
{
    public AA_classView(AA_classViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}