using Orchid.ViewModels;

namespace Orchid.Views;

public partial class ForumListView : ContentPage
{
    public ForumListView(ForumListViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}