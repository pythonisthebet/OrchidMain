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

namespace Orchid.ViewModels
{
    public class AA_filtersViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


        private List<string> filterList;

        public List<string> FilterList
        {
            get { return filterList; }

            set
            {
                filterList = value;
                OnPropertyChanged("FilterList");
            }
        }

        private ObservableCollection<Object> selectedFilters;
        public ObservableCollection<Object> SelectedFilters
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

        private bool isNotEmpty;
        public bool IsNotEmpty
        {
            get
            {
                return this.isNotEmpty;
            }
            set
            {
                this.isNotEmpty = value;
                OnPropertyChanged("IsNotEmpty");
            }
        }

        //private object selectedItem;
        //public object SelectedItem
        //{
        //    get
        //    {
        //        return this.selectedItem;
        //    }
        //    set
        //    {
        //        this.selectedItem = value;
        //        selectedClasses.Add(this);
        //        OnPropertyChanged("SelectedItem");
        //    }
        //}

        //private Color selected_Color;

        //public Color Selected_Color
        //{
        //    get { return selected_Color; }

        //    set
        //    {
        //        selected_Color = value;
        //        OnPropertyChanged("Selected_Color");
        //    }
        //}

        private bool isConfiremed;
        public bool IsConfiremed
        {
            get
            {
                return this.isConfiremed;
            }
            set
            {
                this.isConfiremed = value;
                OnPropertyChanged("IsConfiremed");
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
        public AA_filtersViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            IsNotEmpty = false;
            FilterList = new();
            SelectedFilters = new();
            //selected_Color = Colors.Red;
            isConfiremed = false;
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            this.OrchidService = proxy;
            this.ExternalApiService = proxy2;
        }
        #endregion

        public ICommand Confirm => new Command(OnConfirm);
        public ICommand SelectionChangedCommand => new Command(OnSelectionChanged);

        public async Task InitilizeAsync()
        {
            SelectedFilters.Clear();

            InServerCall = true;
            List<Filter> temp = await OrchidService.GetAllFilters();
            List<string> tempFilterList = new();
            foreach (Filter filter in temp)
            {
                tempFilterList.Add(filter.Fname);
            }
            FilterList = tempFilterList;
            InServerCall = false;
            OnPropertyChanged("SelectedFilters");
        }

        public async void OnSelectionChanged()
        {
            isConfiremed = false;
            //Selected_Color = Colors.Red;
            if (selectedFilters.Count == 0)
            {
                IsNotEmpty = false;
            }
            else
            {
                IsNotEmpty = true;
            }
        }

        public async void OnConfirm()
        {
            InServerCall = true;
            List<string> selectedFilters_String = SelectedFilters.Select(s => (string)s).ToList();
            List<Filter> temp = await OrchidService.GetAllFilters();
            List<Filter> filters = new List<Filter>(temp);
            foreach (var item in temp)
            {
                if (!selectedFilters_String.Contains(item.Fname))
                {
                    filters.Remove(item);
                }
            }
            ChPlusFilters Toupdate = new(((App)Application.Current).CurrentCharacter,filters);
            if (await OrchidService.UpdateCharFilters(Toupdate))
            {
                await Application.Current.MainPage.DisplayAlert("Success!", $"Successfuly saved your Filters!", "ok");

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Success!", $"There was an error please contact the contact email!", "ok");
            }
            InServerCall = false;


        }
    }
}
