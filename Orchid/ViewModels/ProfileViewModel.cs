using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Orchid.Services;
using Orchid.Models;

namespace Orchid.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        #region attributes and properties
        private OrchidWebAPIProxy Orchidservice = new OrchidWebAPIProxy();


        private AppUser currentUser;
        public AppUser CurrentUser
        {
            get
            {
                return ((App)Application.Current).LoggedInUser;
            }
            set
            {
                this.currentUser = value;
                OnPropertyChanged();
            }
        }
        private string newEmail;
        public string NewEmail
        {
            get
            {
                return newEmail;
            }
            set
            {
                this.newEmail = value;
                OnPropertyChanged();
            }
        }
        private string newPass;
        public string NewPass
        {
            get
            {
                return newPass;
            }
            set
            {
                this.newPass = value;
                OnPropertyChanged();
            }
        }
        private string newName;
        public string NewName
        {
            get
            {
                return newName;
            }
            set
            {
                this.newName = value;
                OnPropertyChanged();
            }
        }
        private bool inServerCall = false;
        public bool InServerCall
        {
            get
            {
                return this.inServerCall;
            }
            set
            {
                IsEnabled = !value;
                IsVisible = value;
                this.inServerCall = value;
                OnPropertyChanged();
            }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
                OnPropertyChanged();
            }
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.isEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //constractor
        //initialize the service


        #region changeUserDetails
        //on pressing change on any of the proparties in view
        public ICommand ChangeCommand => new Command(OnChangeCommand);
        #region validations
        private bool ValidateEmail(string Email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ValidatePassword(string pass)
        {
            return pass.Length >= 8 && pass.Any(x => char.IsLetter(x)); // need to inclode one letter
        }
        #endregion
        //on ChangeCommand and getting with param (command parameter)
        //changes properties and update the DB
        async void OnChangeCommand(object param)
        {
            InServerCall = true;
            List<AppUser> users = await Orchidservice.GetAllUsers();
            InServerCall = false;

            switch (param)
            {
                case "email"://change email and check if the same one already esit
                    foreach (AppUser user in users)
                    {
                        if (!ValidateEmail(newEmail))
                        {
                            await Shell.Current.DisplayAlert("Email", $"Email change failed! there is a problem with the email", "ok");
                            return;
                        }
                        if (user.UserEmail == this.newEmail)
                        {
                            await Shell.Current.DisplayAlert("Email", $"Email change failed! A user in the system already uses this mail", "ok");
                            return;
                        }
                    }
                    CurrentUser.UserEmail = this.newEmail;
                    await Shell.Current.DisplayAlert("Email", $"Email change succeeded! reload the page to loke at your new profile", "ok");
                    break;
                case "pass"://change password
                    if (!ValidatePassword(newPass))
                    {
                        await Shell.Current.DisplayAlert("Password", $"Password change failed! the password must be 8 letters log and include at least 1 letter", "ok");
                        return;
                    }
                    CurrentUser.UserPassword = this.newPass;
                    break;
                case "name"://change name
                    foreach (AppUser user in users)
                    {
                        if (NewName == "" || NewName == null)
                        {
                            await Shell.Current.DisplayAlert("Name", $"Name change failed! the name cannot be empty", "ok");
                            return;
                        }
                        if (user.UserName == this.NewName)
                        {
                            await Shell.Current.DisplayAlert("Name", $"Name change failed! The name already exist please chose a different name", "ok");
                            return;
                        }
                    }
                    CurrentUser.UserName = this.newName;
                    break;
            }

            NewEmail = "";
            NewName = "";
            NewPass = "";
            InServerCall = true;
            //update the DB
            bool b = await Orchidservice.UpdateAppUser(CurrentUser);
            InServerCall = false;
            if (!b)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Try again later", "ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("UpdateUser", $"Update seccesful! please open the profile page again to see your new details!", "ok");
                CurrentUser = (((App)Application.Current).LoggedInUser);
            }

        }
        #endregion
    }
}
