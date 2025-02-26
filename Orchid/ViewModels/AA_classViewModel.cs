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

namespace Orchid.ViewModels
{
    public class AA_classViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


        private List<string> classList;

        public List<string> ClassList
        {
            get { return classList; }

            set
            {
                classList = value;
                OnPropertyChanged("ClassList");
            }
        }

        private ObservableCollection<string> selectedClasses;
        public ObservableCollection<string> SelectedClasses
        {
            get
            {
                return this.selectedClasses;
            }
            set
            {
                this.selectedClasses = value;
                OnPropertyChanged("SelectedClasses");
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

        private Color selected_Color;

        public Color Selected_Color
        {
            get { return selected_Color; }

            set
            {
                selected_Color = value;
                OnPropertyChanged("Selected_Color");
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
        public AA_classViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            SelectedClasses = new();
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
            if (SelectedClasses != null)
            {
                SelectedClasses = null;
                SelectedClasses = new();
            }
            ClassList = await ExternalApiService.GetClasses();
            List<Class> templist = await OrchidService.GetAllClasses(((App)Application.Current).CurrentCharacter);
            foreach (Class item in templist)
            {
                SelectedClasses.Add(item.ClassName);
            }
            OnPropertyChanged("SelectedClasses");
        }

        public async void OnSelectionChanged()
        {
            OnPropertyChanged("SelectedClasses");
            Selected_Color = Colors.Red;

        }


        public async void OnConfirm()
        {
            IDictionary<string, object> temp = ((App)Application.Current).CurrentCharacterProperties;
            if (temp.ContainsKey("classes"))
            {
                temp.Remove("classes");
                ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)temp;
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("Classes", selectedClasses.ToList());
            }
            else
            {
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("Classes", selectedClasses.ToList());

            }
            Selected_Color = Colors.LightGreen;



        }


    }
}

