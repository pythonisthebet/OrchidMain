using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Orchid.Models;
using Orchid.Services;
using Orchid.Views;


namespace Orchid.ViewModels
{
    public class AppealViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private IServiceProvider serviceProvider;
        private OrchidWebAPIProxy OrchidService;

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

        private bool isAdmin;
        public bool IsAdmin
        {
            get
            {
                return this.isAdmin;
            }
            set
            {
                this.isAdmin = value;
                OnPropertyChanged("NotIsAdmin");
                OnPropertyChanged("IsAdmin");
            }
        }

        public bool NotIsAdmin
        {
            get
            {
                return !this.IsAdmin;
            }
        }

        private string appeal;
        public string Appeal
        {
            get
            {
                return this.appeal;
            }
            set
            {
                this.appeal = value;
                OnPropertyChanged("Appeal");
            }
        }

        private string banReason;
        public string BanReason
        {
            get
            {
                return this.banReason;
            }
            set
            {
                this.banReason = value;
                OnPropertyChanged("BanReason");
            }
        }

        #endregion

        #region constractor
        public AppealViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.OrchidService = proxy;
        }
        #endregion
        public ICommand SubmitCommand => new Command(OnSubmit);
        public ICommand UnbanCommand => new Command(OnUnban);


        public async Task InitilizeAsync()
        {
            if (((App)Application.Current).LoggedInUser.IsBanned)
            {
                IsAdmin = false;
                BanReason = await OrchidService.GetBanReason(((App)Application.Current).LoggedInUser);
            }
            else
            {
                IsAdmin = true;
                BanReason = await OrchidService.GetBanReason(((App)Application.Current).ReviewUser);
                Appeal = await OrchidService.GetAppeal(((App)Application.Current).ReviewUser);
            }

                OnPropertyChanged("BanReason");
            OnPropertyChanged("Appeal");
        }
        public async void OnSubmit()
        {
            InServerCall = true;
            //from db
            Appeal appeal = new Appeal();
            appeal.UserId = ((App)Application.Current).LoggedInUser.Id;
            appeal.Explanation = Appeal;
            await OrchidService.SetAppeal(appeal);
            await Application.Current.MainPage.DisplayAlert("Success!", $"Successfuly posted your Appeal!", "ok");
            InServerCall = false;
        }

        public async void OnUnban()
        {
            InServerCall = true;
            //from db
            await OrchidService.UnbanUser(((App)Application.Current).ReviewUser);
            await Application.Current.MainPage.DisplayAlert("Success!", $"Successfuly Unbanned the user!", "ok");
            ((App)Application.Current).ReviewUser = new();
            InServerCall = false;
            await Shell.Current.GoToAsync("..");
        }
    }
}
