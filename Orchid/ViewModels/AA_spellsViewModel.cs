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
    public class AA_spellsViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


        private List<string> spellList;

        public List<string> SpellList
        {
            get { return spellList; }

            set
            {
                spellList = value;
                OnPropertyChanged("SpellList");
            }
        }

        private ObservableCollection<Object> selectedSpells;
        public ObservableCollection<Object> SelectedSpells
        {
            get
            {
                return this.selectedSpells;
            }
            set
            {
                this.selectedSpells = value;
                OnPropertyChanged("SelectedSpells");
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
        public AA_spellsViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            SelectedSpells = new();
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
            SelectedSpells.Clear();
            InServerCall = true;
            SpellList = await ExternalApiService.GetDynamicList("spells");
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
                    var clas = temp4.spell;
                    List<string> templist = JsonSerializer.Deserialize<List<string>>(clas);
                    foreach (string item in templist)
                    {
                        SelectedSpells.Add(item);
                    }
                }
                catch (Exception e)
                {
                }
            }
            InServerCall = false;
            OnPropertyChanged("SelectedSpells");
        }

        public async void OnSelectionChanged()
        {
            isConfiremed = false;
            //Selected_Color = Colors.Red;
        }


        public async void OnConfirm()
        {
            IDictionary<string, object> temp = ((App)Application.Current).CurrentCharacterProperties;
            List<string> selectedSpells_String = selectedSpells.Select(s => (string)s).ToList();
            if (temp.ContainsKey("spell"))
            {
                temp.Remove("spell");
                ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)temp;
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("spell", selectedSpells_String.ToList());
            }
            else
            {
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("spell", selectedSpells_String.ToList());

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
