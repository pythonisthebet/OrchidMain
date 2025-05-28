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
    public class AA_imgGeneratorViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private OrchidWebAPIProxy OrchidService;
        private ExternalService ExternalApiService;

        private Character loggedCharacter;
        public Character LoggedCharacter
        {
            get
            {
                return this.loggedCharacter;
            }
            set
            {
                this.loggedCharacter = value;
                OnPropertyChanged("LoggedCharacter");
            }
        }

        private string imageURL;
        public string ImageURL
        {
            get
            {
                return this.imageURL;
            }
            set
            {
                this.imageURL = value;
                OnPropertyChanged("ImageURL");
            }
        }

        private string promptText;
        public string PromptText
        {
            get
            {
                return this.promptText;
            }
            set
            {
                this.promptText = value;
                OnPropertyChanged("PromptText");
            }
        }

        private bool isPremium;
        public bool IsPremium
        {
            get
            {
                return this.isPremium;
            }
            set
            {
                this.isPremium = value;
                OnPropertyChanged("NotIsPremium");
                OnPropertyChanged("IsPremium");
            }
        }

        public bool NotIsPremium
        {
            get
            {
                return !this.IsPremium;
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
        public AA_imgGeneratorViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            IsPremium = false;
            InServerCall = false;
            this.OrchidService = proxy;
            this.ExternalApiService = proxy2;
        }
        #endregion

        public ICommand DefaultGenerate => new Command(OnDefaultGenerate);
        public ICommand ChatGenerate => new Command(OnChatGenerate);


        //checks if the user is premium
        public async Task InitilizeAsync()

        {
            LoggedCharacter = ((App)Application.Current).CurrentCharacter;
            ImageURL = LoggedCharacter.ImgId;
            {
                IsPremium = ((App)Application.Current).LoggedInUser.IsPremium;
            }
        }

        //sets the character image to the default image
        public async void OnDefaultGenerate()
        {
            ((App)Application.Current).CurrentCharacter.ImgId = OrchidService.GetDefaultCharacterImageUrl();
            await OrchidService.UpdateCharacter(((App)Application.Current).CurrentCharacter);
            LoggedCharacter = ((App)Application.Current).CurrentCharacter;
            ImageURL = LoggedCharacter.ImgId;
            OnPropertyChanged("LoggedCharacter");
            await Application.Current.MainPage.DisplayAlert("Image", $"Image saved!", "ok");
        }

        //uses the user given prompt to generate an image using openAIApi and saves the image on the server
        public async void OnChatGenerate()
        {
            InServerCall = true;
            Application.Current.MainPage.DisplayAlert("Prompt", $"Promp Sent! This may take a While.. :<", "ok");
            string temp = ((App)Application.Current).CurrentCharacter.ImgId;
            ((App)Application.Current).CurrentCharacter.ImgId = promptText;
            string result = await OrchidService.SendPrompt(((App)Application.Current).CurrentCharacter);
            if (result == "good")
            {
                List<Character> templ = await OrchidService.GetAllCharacters(((App)Application.Current).LoggedInUser);
                Character real = templ.Where(c => c.Id == loggedCharacter.Id).FirstOrDefault();
                ((App)Application.Current).CurrentCharacter = real;
                LoggedCharacter = ((App)Application.Current).CurrentCharacter;
                await Application.Current.MainPage.DisplayAlert("Image", $"New Image generated and saved!", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Image", $"{result}", "ok");
                ((App)Application.Current).CurrentCharacter.ImgId = temp;
            }
            InServerCall = false;
            LoggedCharacter = ((App)Application.Current).CurrentCharacter;
            ImageURL = LoggedCharacter.ImgId;
            OnPropertyChanged("LoggedCharacter");
            OnPropertyChanged("ImageURL");
        }
    }
}
