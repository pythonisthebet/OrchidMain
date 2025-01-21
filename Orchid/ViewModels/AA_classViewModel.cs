using Orchid.Models;
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

        private Object selectedClass;
        public Object SelectedClass
        {
            get
            {
                return this.selectedClass;
            }
            set
            {
                this.selectedClass = value;
                OnPropertyChanged("SelectedClass");
            }
        }

        #endregion

        public ICommand SingleSelectCommand => new Command(OnSingleSelectChar);

        async void OnSingleSelectChar()
        {
            if (SelectedClass != null)
            {
                Character character = new Character();
                ((App)Application.Current).CurrentCharacter = (string)SelectedClass;
                //Add goto here to show details
                //and edit like in creating a new character 
                await Shell.Current.GoToAsync("createCheracter");

                selectedChar = null;
            }
            else
            {
                await Shell.Current.GoToAsync("//characterCreation/class");
            }



        }


    }
}
}
