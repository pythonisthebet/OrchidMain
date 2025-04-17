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
    public class AA_skillsViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


        private bool isNotEmpty;

        public bool IsNotEmpty
        {
            get { return isNotEmpty; }

            set { isNotEmpty = value; OnPropertyChanged("IsNotEmpty"); }
        }

        private List<string> skillList;

        public List<string> SkillList
        {
            get { return skillList; }

            set
            {
                skillList = value;
                OnPropertyChanged("SkillList");
            }
        }

        private ObservableCollection<Object> selectedSkills;
        public ObservableCollection<Object> SelectedSkills
        {
            get
            {
                return this.selectedSkills;
            }
            set
            {
                this.selectedSkills = value;
                OnPropertyChanged("SelectedSkills");
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
        public AA_skillsViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            SelectedSkills = new();
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
            SelectedSkills.Clear();
            InServerCall = true;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                ExpandoObject templ = ((App)Application.Current).CurrentCharacterProperties;
                dynamic tempr = templ;
                tempr = tempr.Classes;
                List<string> templist1 = JsonSerializer.Deserialize<List<string>>(tempr, options);
                string firstclass = templist1[0];
                SkillList = await ExternalApiService.GetSkills($"{firstclass}");
                ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter(((App)Application.Current).LoggedInUser.Id, ((App)Application.Current).CurrentCharacter);
                IsNotEmpty = true;
                if (dynamicCh != (null))
                {
                    try
                    {
                        dynamic temp = dynamicCh;
                        var temp2 = temp.character;
                        
                        ExpandoObject temp3 = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
                        dynamic temp4 = temp3;
                        var clas = temp4.skill;
                        List<string> templist = JsonSerializer.Deserialize<List<string>>(clas);
                        foreach (string item in templist)
                        {
                            SelectedSkills.Add(item);
                        }
                    }
                    catch (Exception e)
                    {
                        if (SkillList.Count == 0)
                        {
                            IsNotEmpty = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (selectedSkills.Count == 0)
                {
                    IsNotEmpty = false;
                }
                await Application.Current.MainPage.DisplayAlert("Error", $"please select a class first", "ok");
            }
            
            InServerCall = false;
            OnPropertyChanged("SelectedSkills");
        }

        public async void OnSelectionChanged()
        {
            isConfiremed = false;
            //Selected_Color = Colors.Red;
            if (selectedSkills.Count == 0)
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
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            ExpandoObject templ = ((App)Application.Current).CurrentCharacterProperties;
            dynamic tempr = templ;
            tempr = tempr.Classes;
            List<string> templist1 = JsonSerializer.Deserialize<List<string>>(tempr, options);
            string firstclass = templist1[0];


            if (SelectedSkills.Count != await ExternalApiService.GetProficianciesLimits2(firstclass))
            {
                string messege = await ExternalApiService.GetProficianciesLimits(firstclass);
                await Application.Current.MainPage.DisplayAlert("Error", $"{messege}", "ok");
            }
            else
            {
                IDictionary<string, object> tempdic = ((App)Application.Current).CurrentCharacterProperties;
                List<string> selectedSkills_String = selectedSkills.Select(s => (string)s).ToList();
                if (tempdic.ContainsKey("skill"))
                {
                    tempdic.Remove("skill");
                    ((App)Application.Current).CurrentCharacterProperties = (ExpandoObject)tempdic;
                    ((App)Application.Current).CurrentCharacterProperties.TryAdd("skill", selectedSkills_String.ToList());
                }
                else
                {
                    ((App)Application.Current).CurrentCharacterProperties.TryAdd("skill", selectedSkills_String.ToList());

                }
                //Selected_Color = Colors.LightGreen;
                isConfiremed = true;



                //test
                await OrchidService.StoreCharacter(((App)Application.Current).CurrentCharacterProperties, ((App)Application.Current).CurrentCharacter.Id, ((App)Application.Current).LoggedInUser.Id);
                //from json
                ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter(((App)Application.Current).LoggedInUser.Id, ((App)Application.Current).CurrentCharacter);
                dynamic temp1 = dynamicCh;
                var temp2 = temp1.character;
                JsonSerializerOptions options1 = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                ((App)Application.Current).CurrentCharacterProperties = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
                await Application.Current.MainPage.DisplayAlert("Success!", $"Successfuly saved your skills!", "ok");
            }

        }
    }
}
