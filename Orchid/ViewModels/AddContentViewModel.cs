using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Orchid.Models;
using Orchid.Services;
using Orchid.Views;
using Orchid.ViewModels;
using Microsoft.Win32;
using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace Orchid.ViewModels
{
    public class AddContentViewModel : ViewModelBase
    {
        #region attributes and properties
        private OrchidWebAPIProxy OrchidService;
        private SignUpView signupView;

        ObservableCollection<SubType> subTypes;

        public ObservableCollection<SubType> SubTypes
        {
            get { return subTypes; }
            set
            {
                subTypes = value;
                OnPropertyChanged("SubTypes");
            }
        }

        private List<string> typeSource;

        public List<string> TypeSource
        {
            get { return typeSource; }
            set
            {
                typeSource = value;
                OnPropertyChanged("TypeSource");
            }
        }

        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                OnPropertyChanged("ItemName");
            }
        }
        private string itemType;
        public string ItemType
        {
            get { return itemType; }
            set
            {
                itemType = value;
                InitializeListsSubType();
                OnPropertyChanged("ItemType");
                OnPropertyChanged("SubTypes");
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

        

        

        //constractor
        //initialize the properties, attributes and commands
        private IServiceProvider serviceProvider;
        public AddContentViewModel(OrchidWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            this.OrchidService = proxy;
            TypeSource = new List<string>();
            SubTypes = new ObservableCollection<SubType>();
            InitializeListsType();


            

            
        }

        public void InitializeListsSubType()
        {
            #region SubTypes
            SubTypes.Clear();
            switch (ItemType)
            {
                case "Equipment":
                    SubTypes.Add(new SubType("Rarity", "Single", new string[6] { "common", "uncommon", "rare", "very rare", "legendary", "artifact" }));
                    SubTypes.Add(new SubType("Type", "Single", new string[10] { "none", "armour", "potion", "ring", "rod", "scroll", "staff", "wand", "weapon", "Wondrous Item" }));
                    break;

                case "Feats":
                    SubTypes.Add(new SubType("Type", "Single", new string[3] { "general", "class feat", "racial trait" }));
                    break;

                case "races":
                    break;

                case "skills":
                    SubTypes.Add(new SubType("stat", "Single", new string[6] { "str", "dex", "con", "int", "wis", "cha" }));
                    break;

                case "Spells":
                    SubTypes.Add(new SubType("class", "Multiple", new string[11] { "Artificer", "Bard", "Cleric", "Druid", "Fighter(Eldrich Knight)", "Paladin", "Ranger", "Rouge(Arcane Trickster)", "Sorcerer", "Warlock", "Wizard" }));
                    SubTypes.Add(new SubType("stat", "Single", new string[6] { "str", "dex", "con", "int", "wis", "cha" }));

                    break;

                default:
                    break;
            }

            #endregion

        }

        public void InitializeListsType()
        {
            #region TypeSource
            if (TypeSource.Count < 1)
            {
                TypeSource.Add("Equipment");
                TypeSource.Add("Feats");
                TypeSource.Add("races");
                TypeSource.Add("skills");
                TypeSource.Add("Spells");
            }
            #endregion
            
        }
           
    }
}
