using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Orchid.Models;
using Orchid.Services;
using Orchid.Views;
using Orchid.ViewModels;
using Microsoft.Win32;

namespace Orchid.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region attributes and properties
        private OrchidWebAPIProxy OrchidService;
        private SignUpView signupView;

        private string pass;
        public string Pass
        {
            get { return pass; }
            set
            {
                pass = value;
                OnPropertyChanged("Pass");
            }
        }
        private string mail;
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
        }
        private bool inServerCall;
        public bool InServerCall
        {
            get
            {
                return this.inServerCall;
            }
            set
            {
                this.inServerCall = value;
                OnPropertyChanged("NotInServerCall");
                OnPropertyChanged("InServerCall");
            }
        }

        public bool NotInServerCall
        {
            get
            {
                return !this.InServerCall;
            }
        }
        #endregion


        //constractor
        //initialize the properties, attributes and commands
        private IServiceProvider serviceProvider;
        public LoginViewModel(OrchidWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            this.OrchidService = proxy;
            this.LoginCommand = new Command(OnLogin);
            this.SignUpCommand = new Command(GoToSignUp);
        }

        //command on pressing the login button
        public ICommand LoginCommand { get; set; }

        //command on pressing the signup button sends you to to SignUpView
        public Command SignUpCommand { protected set; get; }

        //method
        //activated by the LoginCommand
        //checks with the servise if the given email and password match a user in the DB
        //and if so login to the user and go to profile page in the shell
        private async void OnLogin()
        {
            LoginInfo loginInfo = new(mail, pass);
            //Choose the way you want to blob the page while indicating a server call
            InServerCall = true;
            //await Shell.Current.GoToAsync("connectingToServer");
            AppUser? u = await this.OrchidService.LoginAsync(loginInfo);
            //await Shell.Current.Navigation.PopModalAsync();
            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {

                await Application.Current.MainPage.DisplayAlert("Login", "Login Failed!", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", $"Login Succeed!", "ok");
                u = null;
                Mail = "";
                Pass = "";


                AppShell shell = serviceProvider.GetService<AppShell>();
                Ch_ListViewModel c = serviceProvider.GetService<Ch_ListViewModel>();
                //Ch_ListViewModel.Refresh(); //Refresh data and user in the tasksview model as it is a singleton
                ((App)Application.Current).MainPage = shell;
                Shell.Current.FlyoutIsPresented = false; //close the flyout
                await Shell.Current.GoToAsync("Ch_List"); //Navigate to the Tasks tab page
            }
        }

        //method
        //activated by SignUpCommand
        //send you to SignUpView
        private async void GoToSignUp()
        {
            await ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<SignUpView>());
        }
    }
}
