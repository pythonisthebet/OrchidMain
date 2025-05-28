using Orchid.Models;
using Orchid.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Text.Json;
using iText.StyledXmlParser.Jsoup.Safety;
using Orchid.Views;

namespace Orchid.ViewModels
{
    public class UserListAViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


        private List<AppUser> userList;

        public List<AppUser> UserList
        {
            get { return userList; }

            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }

        private Object selectedUser;
        public Object SelectedUser
        {
            get
            {
                return this.selectedUser;
            }
            set
            {
                this.selectedUser = value;
                OnPropertyChanged("SelectedUser");
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
        #region constructor
        private IServiceProvider serviceProvider;
        public UserListAViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            SelectedUser = new();
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            this.OrchidService = proxy;
            this.ExternalApiService = proxy2;
        }
        #endregion

        //load data from server

        public async Task InitilizeAsync()
        {
            InServerCall = true;
            UserList = await OrchidService.GetAllUsers();
            InServerCall = false;
            OnPropertyChanged("SelectedAppeal");
        }
        public ICommand SingleSelectCommand => new Command(OnSingleSelectUser);

        //sends to profile page of selected user
        public async void OnSingleSelectUser()
        {
            ((App)Application.Current).ReviewUser = (AppUser)selectedUser;
            await ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<ProfileView>());


        }
    }
}
