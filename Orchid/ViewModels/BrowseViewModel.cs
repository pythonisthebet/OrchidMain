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

namespace Orchid.ViewModels
{
    public class BrowseViewModel : ViewModelBase
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

        private string selectedFilters;
        public string SelectedFilters
        {
            get
            {
                return this.selectedFilters;
            }
            set
            {
                this.selectedFilters = value;
                OnPropertyChanged("SelectedFilters");
            }
        }
        #endregion

        #region constractor
        public BrowseViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.OrchidService = proxy;
        }
        #endregion
        public ICommand Select => new Command(OnSelect);

        public async void OnSelect()
        {

        }
        public async Task<List<string>> InitilizeAsync()
        {
            List<Filter> filtersDB = await OrchidService.GetAllFilters();
            List<string> FiltersForApp = new List<string>();
            foreach (Filter filter in filtersDB)
            {
                FiltersForApp.Add(filter.Fname);
            }
            return FiltersForApp;
        }
    }
}
