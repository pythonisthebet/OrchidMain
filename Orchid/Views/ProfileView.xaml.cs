using Orchid.ViewModels;

namespace Orchid.Views;

public partial class ProfileView : ContentPage
{
    public ProfileView(ProfileViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
    protected override async void OnDisappearing()
    {
        ((App)Application.Current).ReviewUser = ((App)Application.Current).ReviewUserDefault;
    }
}