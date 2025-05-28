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
using System.Collections;

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
            IsNotEmpty = false;
            SelectedSpells = new();
            SpellList = [];
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


        //load from the External API
        public async Task InitilizeAsync()
        {
            IDictionary<int,List<string>> tempSpellListPlusLevel = new Dictionary<int, List<string>>();
            SelectedSpells.Clear();
            InServerCall = true;
            List<string> templist = new List<string>();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter(((App)Application.Current).LoggedInUser.Id, ((App)Application.Current).CurrentCharacter);
            if (dynamicCh != (null))
            {
                try
                {
                    dynamic temp = dynamicCh;
                    var temp2 = temp.character;

                    ExpandoObject temp3 = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
                    dynamic temp4 = temp3;
                    var clas = temp4.Classes;
                    templist = JsonSerializer.Deserialize<List<string>>(clas);
                }
                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"please select a class first", "ok");
                }
                try
                {
                    bool isRunning = true;
                    for (int i = 0; i < 10; i++)
                    {
                        tempSpellListPlusLevel.Add(i, new List<string>());
                    }
                    while (isRunning)
                    {
                        string tempClass = templist.FirstOrDefault();
                        templist.Remove(tempClass);
                        ClassSpellsPlusCount tempClassSpells = await ExternalApiService.GetClassSpells(tempClass);
                        foreach (var tempSpell in tempClassSpells.results)
                        {
                            if (!tempSpellListPlusLevel[tempSpell.level].Contains(tempSpell.name))
                            {
                                tempSpellListPlusLevel[tempSpell.level].Add(tempSpell.name);
                            }
                        }
                        if (templist.Count == 0)
                        {
                            isRunning = false;
                        }
                    }
                    isRunning = true;
                    List<string> temp = new List<string>();
                    for (int i = 0; i < 10; i++)
                    {
                        temp.Add($"Level {i} -------------------");
                        temp.AddRange(tempSpellListPlusLevel[i]);
                    }
                    SpellList = temp;

                }
                catch (Exception e)
                {
                }
                try
                {
                    dynamic temp = dynamicCh;
                    var temp2 = temp.character;

                    ExpandoObject temp3 = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
                    dynamic temp4 = temp3;
                    var clas = temp4.Spells;
                    List<string> tempSlist = JsonSerializer.Deserialize<List<string>>(clas);
                    foreach (string item in tempSlist)
                    {
                        SelectedSpells.Add(item);
                    }
                }
                catch (Exception e)
                {
                    if (SpellList.Count == 0)
                    {
                        IsNotEmpty = false;
                    }
                }
            }
            else
            {

            }
            if (spellList.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Alert!", $"None of your classes can cast spells", "ok");
                IsNotEmpty = false;
            }
            else
            {
                IsNotEmpty = true;
            }
            InServerCall = false;
            OnPropertyChanged("SpellList");
            OnPropertyChanged("SelectedSpells");
        }

        //disable or enable the confirm button
        public async void OnSelectionChanged()
        {
            isConfiremed = false;
            //Selected_Color = Colors.Red;
            if (selectedSpells.Count == 0)
            {
                IsNotEmpty = false;
            }
            else
            {
                IsNotEmpty = true;
            }
        }

        //save the selected data to characters json file on the server
        public async void OnConfirm()
        {
            IDictionary<string, object> temp = ((App)Application.Current).CurrentCharacterProperties;
            List<string> selectedSpells_String = selectedSpells.Select(s => (string)s).ToList();
            if (!selectedSpells_String.Any(item => item.Contains("level", StringComparison.OrdinalIgnoreCase)))
            {
                if (temp.ContainsKey("Spells"))
                {
                    temp.Remove("Spells");
                    ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)temp;
                    ((App)Application.Current).CurrentCharacterProperties.TryAdd("Spells", selectedSpells_String.ToList());
                }
                else
                {
                    ((App)Application.Current).CurrentCharacterProperties.TryAdd("Spells", selectedSpells_String.ToList());

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
                await Application.Current.MainPage.DisplayAlert("Success!", $"Successfuly saved your spells!", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert!", $"Please do not select the level indicator!", "ok");
            }


            /*if (SelectedClasses != null)
            {
                ((App)Application.Current).)
                items.TryAdd("selectedClasses", SelectedClasses);
                //need to add the level of each class


            }*/




        }
    }
}
