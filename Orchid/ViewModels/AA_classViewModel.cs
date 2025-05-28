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
        public AA_classViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            IsNotEmpty = false;
            SelectedClasses = new();
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

        //enable or disable the confirm button
        public async void OnSelectionChanged()
        {
            isConfiremed = false;
            if (selectedClasses.Count == 0)
            {
                IsNotEmpty = false;
            }
            else
            {
                IsNotEmpty = true;
            }
            //Selected_Color = Colors.Red;
        }


        //loads data from the external api
        public async Task InitilizeAsync()
        {
            SelectedClasses.Clear();
            InServerCall = true;
            ClassList = await ExternalApiService.GetDynamicList("classes");
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
                    var clas = temp4.Classes;
                    List<string> templist = JsonSerializer.Deserialize<List<string>>(clas);
                    foreach (string item in templist)
                    {
                        SelectedClasses.Add(item);
                    }
                }
                catch (Exception)
                {
                }
            }
            InServerCall = false;
            OnPropertyChanged("SelectedClasses");
        }



        //save the selected data to characters json file on the server
        public async void OnConfirm()
        {
            IDictionary<string, object> temp = ((App)Application.Current).CurrentCharacterProperties;
            List<string> selectedClasses_String = selectedClasses.Select(s => (string)s).ToList();
            List<string> selectedClasseslevel_String = new List<string>();
            int sum = 0;
            bool selectingLevels = true;
            bool NoError = true;
            while (selectingLevels)
            {
                foreach (string item in selectedClasses_String)
                {
                    NoError = false;
                    while (NoError == false)
                    {
                        try
                        {
                            string result = await Application.Current.MainPage.DisplayPromptAsync("Level Selection", "What level for class " + item, initialValue: "1", maxLength: 2, keyboard: Keyboard.Numeric);
                            int number = int.Parse(result);
                            if (number > 20 || number < 1) 
                            {
                                number = int.Parse("&");
                            }
                            sum += number;
                            selectedClasseslevel_String.Add(result);
                            NoError = true;
                        }
                        catch (Exception)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", $"Please input a number (0-20)", "ok");
                        }

                    }
                }
                if (sum > 20)
                {
                    sum = 0;
                    await Application.Current.MainPage.DisplayAlert("Error", $"sum of levels cannot exceed 20", "ok");
                    selectedClasseslevel_String.Clear();
                }
                else 
                { 
                    selectingLevels = false;
                }
            }

            if (temp.ContainsKey("Classes"))
            {
                temp.Remove("Classes");
                ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)temp;
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("Classes", selectedClasses_String.ToList());
            }
            else
            {
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("Classes", selectedClasses_String.ToList());

            }
            if (temp.ContainsKey("ClassLevels"))
            {
                temp.Remove("ClassLevels");
                ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)temp;
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("ClassLevels", selectedClasseslevel_String.ToList());
            }
            else
            {
                ((App)Application.Current).CurrentCharacterProperties.TryAdd("ClassLevels", selectedClasseslevel_String.ToList());

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
            await Application.Current.MainPage.DisplayAlert("Success!", $"Successfuly saved your Classes!", "ok");


            /*if (SelectedClasses != null)
            {
                ((App)Application.Current).)
                items.TryAdd("selectedClasses", SelectedClasses);
                //need to add the level of each class


            }*/




        }


    }
}

