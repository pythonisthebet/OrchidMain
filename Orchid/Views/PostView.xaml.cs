using Orchid.ViewModels;

namespace Orchid.Views;

public partial class PostView : ContentPage
{
    public PostView(PostViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}