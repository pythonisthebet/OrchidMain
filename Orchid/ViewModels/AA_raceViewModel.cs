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
    public class AA_raceViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


        private List<string> raceList;

        public List<string> RaceList
        {
            get { return raceList; }

            set
            {
                raceList = value;
                OnPropertyChanged("RaceList");
            }
        }

        private Object selectedRace;
        public Object SelectedRace
        {
            get
            {
                return this.selectedRace;
            }
            set
            {
                this.selectedRace = value;
                OnPropertyChanged("SelectedRace");
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
        public AA_raceViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            SelectedRace = new();
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
            if (SelectedRace != null)
            {
                SelectedRace = null;
                SelectedRace = new();
            }
            RaceList = await ExternalApiService.GetRaces();
            Race temprace = await OrchidService.GetRace(((App)Application.Current).CurrentCharacter);
            if (temprace != null)
            {
                SelectedRace = temprace;
            }
            OnPropertyChanged("SelectedRace");
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
            if (SelectedRace != null)
            {
                await OrchidService.RemoveRace(((App)Application.Current).CurrentCharacter);


                Race linkedRace = new(((App)Application.Current).CurrentCharacter.Id, (string)selectedRace);
                await OrchidService.AddRace(linkedRace);

                //Add goto here to show details
                //and edit like in creating a new character 


            }

            //SelectedClasses = null;
            //selectedClasses = new();

        }
    }
}
