using Orchid.Models;
using Orchid.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private ObservableCollection<Object> selectedClasses;
        public ObservableCollection<Object> SelectedClasses
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
            ClassList = await ExternalApiService.GetDynamicList("classes");
            List<Class> templist = await OrchidService.GetAllClasses(((App)Application.Current).CurrentCharacter);
            foreach (Class item in templist)
            {
                SelectedClasses.Add(item.ClassName);
            }
            OnPropertyChanged("SelectedClasses");
        }

        public async void OnSelectionChanged(object character)
        {
            //OnPropertyChanged("SelectedClasses");

            //List<Class> templist = await OrchidService.GetAllClasses(((App)Application.Current).CurrentCharacter);
            //foreach (Class item in templist)
            //{
            //    SelectedClasses.Add(item.ClassName);
            //}
        }


        public async void OnConfirm()
        {
            if (SelectedClasses != null)
            {
                await OrchidService.RemoveClasses(((App)Application.Current).CurrentCharacter);

                foreach (string item in SelectedClasses)
                {
                    Class linkedClass = new(((App)Application.Current).CurrentCharacter.Id, item);
                    await OrchidService.AddClass(linkedClass);
                }
                //Add goto here to show details
                //and edit like in creating a new character 


            }

        //    Ch_obj_json new_ch = new Ch_obj_json();
        //    Class_obj[] class_list = new Class_obj[selectedClasses.Count];
        //    int count = 0;
        //    foreach (Class item in selectedClasses)
        //    {
        //        class_list[count].name += item.ClassName;
        //        class_list
        //{

        //}

        //    }
        //    new_ch.class_Obj = new Class_obj[selectedClasses.Count]();
            //SelectedClasses = null;
            //selectedClasses = new();

        }


    }
}

