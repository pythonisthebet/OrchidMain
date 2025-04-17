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
    public class AA_equipmentViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


        private List<string> equipmentList;

        public List<string> EquipmentList
        {
            get { return equipmentList; }

            set
            {
                equipmentList = value;
                OnPropertyChanged("EquipmentList");
            }
        }

        private ObservableCollection<Object> selectedEquipment;
        public ObservableCollection<Object> SelectedEquipment
        {
            get
            {
                return this.selectedEquipment;
            }
            set
            {
                this.selectedEquipment = value;
                OnPropertyChanged("SelectedEquipment");
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
        public AA_equipmentViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            SelectedEquipment = new();
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
            SelectedEquipment.Clear();
            InServerCall = true;
            EquipmentList = await ExternalApiService.GetDynamicList("equipment");
            ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter(((App)Application.Current).LoggedInUser.Id, ((App)Application.Current).CurrentCharacter);
            if (dynamicCh != (null))
            {
                try
                {
                    dynamic temp = dynamicCh;
                    var temp2 = temp.character;
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ExpandoObject temp3 = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
                    dynamic temp4 = temp3;
                    var clas = temp4.Equipment;
                    List<string> templist = JsonSerializer.Deserialize<List<string>>(clas);
                    foreach (string item in templist)
                    {
                        SelectedEquipment.Add(item);
                    }
                }
                catch (Exception)
                {
                }
            }
            InServerCall = false;
            OnPropertyChanged("SelectedEquipment");
        }

        public async void OnSelectionChanged()
        {
            isConfiremed = false;
            //Selected_Color = Colors.Red;
            if (selectedEquipment.Count == 0)
            {
                isNotEmpty = false;
            }
            else
            {
                isNotEmpty = true;
            }
        }


        public async void OnConfirm()
        {
            IDictionary<string, object> temp = ((App)Application.Current).CurrentCharacterProperties;
            List<string> selectedEquipment_String = selectedEquipment.Select(s => (string)s).ToList();
            if (temp.ContainsKey("equipment"))
            {
                temp.Remove("equipment");
                ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)temp;
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("equipment", selectedEquipment_String.ToList());
            }
            else
            {
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("equipment", selectedEquipment_String.ToList());

            }
            //Selected_Color = Colors.LightGreen;
            isConfiremed = true;



            //test
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
            await Application.Current.MainPage.DisplayAlert("Success!", $"Successfuly saved your equipment!", "ok");


            /*if (SelectedClasses != null)
            {
                ((App)Application.Current).)
                items.TryAdd("selectedClasses", SelectedClasses);
                //need to add the level of each class


            }*/




        }
    }
}
