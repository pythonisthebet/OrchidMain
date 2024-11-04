using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_subclassView : ContentPage
{
    public AA_subclassView(AA_subclassViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}