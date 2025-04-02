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
    public class AA_featsViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


        private List<string> featList;

        public List<string> FeatList
        {
            get { return featList; }

            set
            {
                featList = value;
                OnPropertyChanged("FeatList");
            }
        }

        private ObservableCollection<Object> selectedFeats;
        public ObservableCollection<Object> SelectedFeats
        {
            get
            {
                return this.selectedFeats;
            }
            set
            {
                this.selectedFeats = value;
                OnPropertyChanged("SelectedFeats");
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
        public AA_featsViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            SelectedFeats = new();
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
            SelectedFeats.Clear();
            InServerCall = true;
            FeatList = await ExternalApiService.GetDynamicList("feats");
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
                    var clas = temp4.feat;
                    List<string> templist = JsonSerializer.Deserialize<List<string>>(clas);
                    foreach (string item in templist)
                    {
                        SelectedFeats.Add(item);
                    }
                }
                catch (Exception e)
                {
                }
            }
            InServerCall = false;
            OnPropertyChanged("SelectedFeats");
        }

        public async void OnSelectionChanged()
        {
            isConfiremed = false;
            //Selected_Color = Colors.Red;
        }


        public async void OnConfirm()
        {
            IDictionary<string, object> temp = ((App)Application.Current).CurrentCharacterProperties;
            List<string> selectedFeats_String = selectedFeats.Select(s => (string)s).ToList();
            if (temp.ContainsKey("feat"))
            {
                temp.Remove("feat");
                ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)temp;
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("feat", selectedFeats_String.ToList());
            }
            else
            {
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("feat", selectedFeats_String.ToList());

            }
            //Selected_Color = Colors.LightGreen;
            isConfiremed = true;



            //test
            await OrchidService.StoreCharacter(((App)Application.Current).CurrentCharacterProperties, ((App)Application.Current).CurrentCharacter.Id, ((App)Application.Current).LoggedInUser.Id);



            /*if (SelectedClasses != null)
            {
                ((App)Application.Current).)
                items.TryAdd("selectedClasses", SelectedClasses);
                //need to add the level of each class


            }*/




        }
    }
}
