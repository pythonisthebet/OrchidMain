using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_imgGeneratorView : ContentPage
{
    public AA_imgGeneratorView(AA_imgGeneratorViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}