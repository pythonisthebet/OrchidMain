using Orchid.Models;
using Orchid.Views;
using Orchid.ViewModels;


namespace Orchid
{
    public partial class App : Application
    {

        public User LoggedInUser { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
