using Microsoft.Maui.ApplicationModel.Communication;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Orchid.Models;
using Orchid.Services;
using Orchid.ViewModels;


namespace Orchid.ViewModels
{
    public class SignUpViewModel: ViewModelBase
    {
        #region FormValidation
        #region שם
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }
        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion
        #region password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }
        private void ValidatePassword()
        {
            this.ShowPasswordError = (Password == null) || Password.Length < 8 || !Password.Any(x => char.IsLetter(x)); // need to inclode one letter  
        }
        #endregion
        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }
        private void ValidateEmail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (match.Success)
            {
                ShowEmailError = false;
            }
            else
            {
                ShowEmailError = true;
            }
                
        }
        #endregion
        #region Private policy
        private bool showCheckBoxError;

        public bool ShowCheckBoxError
        {
            get => showCheckBoxError;
            set
            {
                showCheckBoxError = value;
                OnPropertyChanged("ShowCheckBoxError");
            }
        }

        private bool checkBox;

        public bool CheckBox
        {
            get => checkBox;
            set
            {
                checkBox = value;
                ValidatePassword();
                OnPropertyChanged("checkBox");
            }
        }

        private string checkBoxError;

        public string CheckBoxError
        {
            get => checkBoxError;
            set
            {
                checkBoxError = value;
                OnPropertyChanged("CheckBoxError");
            }
        }
        private void ValidateCheckBox()
        {
            if(CheckBox == true)
            {
                ShowCheckBoxError = false;
            }
            else
            {
                ShowCheckBoxError = true;
            }
           
        }
        #endregion
        //on sighing up
        public Command SaveDataCommand { protected set; get; }

        //the service
        private OrchidWebAPIProxy OrchidService;

        //constractor initializing all the errors the service and the command
        public SignUpViewModel(OrchidWebAPIProxy service)
        {
            this.NameError = "This is must";
            this.ShowNameError = false;
            this.PasswordError = "The password must include at least 8 digits and with at least 1 letter";
            this.ShowPasswordError = false;
            this.EmailError = "The email was not found";
            this.ShowEmailError = false;
            this.CheckBoxError = "Please confirm our private policy";
            this.ShowCheckBoxError = false;
            this.SaveDataCommand = new Command(() => SaveData());
            this.OrchidService = service;
            ;
        }
        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidatePassword();
            ValidateEmail();
            ValidateName();
            ValidateCheckBox();

            //check if any validation failed
            if (ShowPasswordError || ShowEmailError || ShowCheckBoxError||
                ShowNameError)
                return false;
            return true;
        }

        //method 
        // if the data is valid (accepted by ValidateForm) then register the user in the DB via the servise 
        private async void SaveData()
        {
            if (ValidateForm())
            {
                AppUser u = new AppUser();
                u.UserEmail = this.Email;
                u.UserPassword = this.Password;
                u.UserName = this.Name;
                u.IsAdmin = false;
                u.IsPremium = false;
                if (null != await this.OrchidService.Register(u)) //awaiting webapiproxy function register
                {
                    this.Email = "";
                    this.Password = "";
                    this.Name = "";
                    await App.Current.MainPage.DisplayAlert("Save data", "your data is saved", "אישור", FlowDirection.RightToLeft);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Save data", "there is a problem with your data", "אישור", FlowDirection.RightToLeft);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Save data", "there is a problem with your data", "אישור", FlowDirection.RightToLeft);
            }
                
        }
        #endregion
    }
}
