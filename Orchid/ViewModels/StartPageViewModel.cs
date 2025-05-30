using Microsoft.Extensions.DependencyInjection;
using Orchid.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.ViewModels
{
    public class StartPageViewModel
    {
        private IServiceProvider serviceProvider;

        public Command SignUpCommand { protected set; get; }

        public Command LoginCommand { protected set; get; }

        public StartPageViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.LoginCommand = new Command(GoToLogIn);
            this.SignUpCommand = new Command(GoToSignUp);
        }

        //send you to SignUpView
        private async void GoToSignUp()
        {
            await ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<SignUpView>());
        }

        private async void GoToLogIn()
        {
            await ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<LoginView>());
        }

    }
}
