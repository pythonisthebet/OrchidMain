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
using iText.StyledXmlParser.Jsoup.Safety;
using Orchid.Views;

namespace Orchid.ViewModels
{
    public class CharacterListAViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;


        private List<Character> characterList;

        public List<Character> CharacterList
        {
            get { return characterList; }

            set
            {
                characterList = value;
                OnPropertyChanged("CharacterList");
            }
        }

        private Object selectedCharacter;
        public Object SelectedCharacter
        {
            get
            {
                return this.selectedCharacter;
            }
            set
            {
                this.selectedCharacter = value;
                OnPropertyChanged("SelectedCharacter");
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
        public CharacterListAViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            SelectedCharacter = new();
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            this.OrchidService = proxy;
            this.ExternalApiService = proxy2;
        }
        #endregion

        //loads data from the database
        public async Task InitilizeAsync()
        {
            InServerCall = true;
            CharacterList = await OrchidService.GetAllCharacters();
            InServerCall = false;
            OnPropertyChanged("SelectedCharacter");
        }
        public ICommand SingleSelectCommand => new Command(OnSingleSelectCharacter);


        //go to CharacterSheetViewModel of the selected character
        public async void OnSingleSelectCharacter()
        {
            ((App)Application.Current).CurrentCharacter = (Character)selectedCharacter;
            ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter((int)((App)Application.Current).CurrentCharacter.UserId, ((App)Application.Current).CurrentCharacter);
            dynamic temp = dynamicCh;
            var temp2 = temp.character;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            ((App)Application.Current).CurrentCharacterProperties = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);
            await ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<CharacterSheetPage>());


        }
    }
}
