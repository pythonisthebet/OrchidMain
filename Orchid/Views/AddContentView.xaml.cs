using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AddContentView : ContentPage
{
    public AddContentView(AddContentViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}