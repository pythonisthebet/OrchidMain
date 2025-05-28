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
            IsNotEmpty = false;
            SelectedRace = new();
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            this.OrchidService = proxy;
            this.ExternalApiService = proxy2;
        }
        #endregion

        public ICommand Confirm => new Command(OnConfirm);
        public ICommand SelectionChangedCommand => new Command(OnSelectionChanged);


        //load data from the external api
        public async Task InitilizeAsync()

        {
            {
                SelectedRace = new object();
                InServerCall = true;
                RaceList = await ExternalApiService.GetDynamicList("races");
                ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter(((App)Application.Current).LoggedInUser.Id, ((App)Application.Current).CurrentCharacter);
                try
                {
                    if (dynamicCh != (null))
                    {
                        dynamic temp = dynamicCh;
                        var temp2 = temp.character;
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        ExpandoObject temp3 = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
                        dynamic temp4 = temp3;
                        var race = temp4.race;
                        string stringrace = JsonSerializer.Deserialize<string>(temp4.race, options);
                        SelectedRace = stringrace;
                    }
                }
                catch (Exception)
                { }
                
                InServerCall = false;
                OnPropertyChanged("SelectedRace");
            }
        }

        //enable or diable the confirm button
        public async void OnSelectionChanged()
        {
            try
            {
                if (SelectedRace.ToString() == "System.Object")
                {
                    IsNotEmpty = false;
                }
                else
                {
                    IsNotEmpty = true;
                }
            }
            catch (Exception)
            {
                IsNotEmpty = false;
            }
        }

        //save the selected data to characters json file on the server
        public async void OnConfirm()
        {
            if (SelectedRace != null)
            {
                IDictionary<string, object> temp = ((App)Application.Current).CurrentCharacterProperties;
                if (temp.ContainsKey("race"))
                {
                    temp.Remove("race");
                    ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)temp;
                    ((App)Application.Current).CurrentCharacterProperties.TryAdd("race", SelectedRace.ToString());
                }
                else
                {
                    ((App)Application.Current).CurrentCharacterProperties.TryAdd("race", SelectedRace.ToString());

                }

                ////test
                await OrchidService.StoreCharacter(((App)Application.Current).CurrentCharacterProperties, ((App)Application.Current).CurrentCharacter.Id, ((App)Application.Current).LoggedInUser.Id);
                //from json
                ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter(((App)Application.Current).LoggedInUser.Id, ((App)Application.Current).CurrentCharacter);
                dynamic temp1 = dynamicCh;
                var temp2 = temp1.character;
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                ((App)Application.Current).CurrentCharacterProperties = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
                await Application.Current.MainPage.DisplayAlert("Success!", $"Successfuly saved your race!", "ok");


            }

            //SelectedClasses = null;
            //selectedClasses = new();

        }
    }
}
