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
    public class Ch_ListViewModel : ViewModelBase
    {
        #region Attributes and Properties

        private OrchidWebAPIProxy OrchidService;

        private List<Character> chList;

        public List<Character> ChList 
        {
            get { return chList; }

            set
            {
                chList = value;
                OnPropertyChanged("ChList");
            }
        }

        private Object selectedChar;
        public Object SelectedChar
        {
            get
            {
                return this.selectedChar;
            }
            set
            {
                this.selectedChar = value;
                OnPropertyChanged("SelectedChar");
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
        private OrchidWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public Ch_ListViewModel(OrchidWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            this.OrchidService = proxy;
        }
        #endregion
        public ICommand SingleSelectCommand => new Command(OnSingleSelectChar);

        async void OnSingleSelectChar()
        {
            if (SelectedChar != null)
            {
                ((App)Application.Current).CurrentCharacter = (Character)SelectedChar;
                //Add goto here to show details
                //and edit like in creating a new character 
                await Shell.Current.GoToAsync("createCheracter");

                selectedChar = null;
            }
            else
            {
                string answer = await Application.Current.MainPage.DisplayPromptAsync("Required query", "What's The Characters name?");
                Character character = new(answer, ((App)Application.Current).CurrentCharacter.Id);
                Character? u = await this.OrchidService.CreateCharacter(character);
                ((App)Application.Current).CurrentCharacter = u;
                await Shell.Current.GoToAsync("//characterCreation/class");
            }
                


        }


    }
}
