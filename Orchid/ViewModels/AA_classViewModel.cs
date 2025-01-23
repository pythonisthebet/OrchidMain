using Orchid.Models;
using Orchid.Services;
using System;
using System.Collections.Generic;
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

        private List<string> selectedClasses;
        public List<string> SelectedClasses
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
        OrchidWebAPIProxy proxy;
        ExternalService proxy2;
        public AA_classViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            this.OrchidService = proxy;
            this.ExternalApiService = proxy2;
        }
        #endregion

        public ICommand Confirm => new Command(OnConfirm);


        public async Task InitilizeAsync()

        {
            ClassList = await proxy2.GetClasses();
            List<Class> templist = await proxy.GetAllClasses(((App)Application.Current).CurrentCharacter);
            foreach (Class item in templist)
            {
                selectedClasses.Add(item.ClassName);
            }
        }
        



        async void OnConfirm()
        {
            if (SelectedClasses != null)
            {
                await proxy.RemoveClasses(((App)Application.Current).CurrentCharacter.Id);

                foreach (string item in SelectedClasses)
                {
                    Class linkedClass = new(((App)Application.Current).CurrentCharacter.Id, item);
                    await proxy.AddClass(linkedClass);
                }
                //Add goto here to show details
                //and edit like in creating a new character 


                selectedClasses = null;
            }




        }
    }
}

