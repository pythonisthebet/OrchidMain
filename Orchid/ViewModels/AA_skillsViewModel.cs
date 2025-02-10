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
    public class AA_skillsViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


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

        private int selectedSkillsCount;
        public int SelectedSkillsCount
        {
            get
            {
                return this.selectedSkillsCount;
            }
            set
            {
                this.selectedSkillsCount = value;
                OnPropertyChanged("SelectedSkillsCount");
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
        public AA_skillsViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            SelectedSkills = new();
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            this.OrchidService = proxy;
            this.ExternalApiService = proxy2;
        }
        #endregion

        public ICommand Confirm => new Command(OnConfirm);
        //public ICommand SelectionChangedCommand => new Command(OnSelectionChanged);



        public async Task InitilizeAsync()

        {
            if (SelectedSkills != null)
            {
                SelectedSkills = null;
                SelectedSkills = new();
            }
            Class selectedClass = OrchidService.GetAllClasses(((App)Application.Current).CurrentCharacter).Result.FirstOrDefault();

            (List<string> list, int count) SkillListpluscount = await ExternalApiService.GetSkills(selectedClass);
            SkillList = SkillListpluscount.list;
            selectedSkillsCount = SkillListpluscount.count;

            List<string> templist = await OrchidService.GetAllSkills(((App)Application.Current).CurrentCharacter);
            foreach(string item in templist)
            {
                SelectedSkills.Add(item);
            }
            OnPropertyChanged("SelectedSkills");
        }

        //public async void OnSelectionChanged(object character)
        //{
        //    OnPropertyChanged("SelectedClasses");

        //    List<Class> templist = await OrchidService.GetAllClasses(((App)Application.Current).CurrentCharacter);
        //    foreach (Class item in templist)
        //    {
        //        SelectedClasses.Add(item.ClassName);
        //    }
        //}


        public async void OnConfirm()
        {
            if (SelectedSkills != null)
            {
                await OrchidService.RemoveSkills(((App)Application.Current).CurrentCharacter);

                foreach (string item in SelectedSkills)
                {
                    await OrchidService.AddSkill(((App)Application.Current).CurrentCharacter, item);
                }
                //Add goto here to show details
                //and edit like in creating a new character 


            }

            //SelectedClasses = null;
            //selectedClasses = new();

        }
    }
}
