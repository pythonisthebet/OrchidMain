using Orchid.ViewModels;

namespace Orchid.Views;

public partial class ProfileView : ContentPage
{
    public ProfileView(ProfileViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}