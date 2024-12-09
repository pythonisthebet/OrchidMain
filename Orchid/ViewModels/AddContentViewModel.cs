using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Orchid.Models;
using Orchid.Services;
using Orchid.Views;
using Orchid.ViewModels;
using Microsoft.Win32;

namespace Orchid.ViewModels
{
    public class AddContentViewModel : ViewModelBase
    {
        #region attributes and properties
        private OrchidWebAPIProxy OrchidService;
        private SignUpView signupView;
        List<string> TypeSource = new List<string>();
        List<SubType> SubTypes = new List<SubType>();


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
                OnPropertyChanged("ItemType");
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
            
            this.LoginCommand = new Command(OnLogin);
            this.SignUpCommand = new Command(GoToSignUp);

            

            
        }

        public void InitializeLists()
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

            #region SubTypes
            SubTypes.Clear();
            switch (ItemType)
            {
                case "Equipment":
                    SubTypes.Add(new SubType("Rarity", "Single", new string[6] { "common", "uncommon", "rare", "very rare", "legendary", "artifact" }));
                    SubTypes.Add(new SubType("Type", "Single", new string[10] { "none","armour", "potion", "ring", "rod", "scroll", "staff","wand","weapon","Wondrous Item"}));
                    break;

                case "Feats":
                    SubTypes.Add(new SubType("Type", "Single", new string[3] { "general", "class feat" , "racial trait"}));
                    break;

                case "races":
                    break;

                case "skills":
                    SubTypes.Add(new SubType("stat", "Single", new string[6] { "str", "dex", "con", "int", "wis", "cha"}));
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
    }
}
