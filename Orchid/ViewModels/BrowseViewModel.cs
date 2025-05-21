using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Orchid.Models;
using Orchid.Services;
using Orchid.Views;

namespace Orchid.ViewModels
{
    public class BrowseViewModel : ViewModelBase
    {
        #region Attributes and Properties
        private IServiceProvider serviceProvider;
        private OrchidWebAPIProxy OrchidService;

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

        private string selectedFilters;
        public string SelectedFilters
        {
            get
            {
                return this.selectedFilters;
            }
            set
            {
                this.selectedFilters = value;
                OnPropertyChanged("SelectedFilters");
            }
        }

        private Character selectedChar;
        public Character SelectedChar
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
        #endregion

        #region constractor
        public BrowseViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.OrchidService = proxy;
        }
        #endregion
        public ICommand Select => new Command(OnSelect);

        public async void OnSelect()
        {
            //from db
            ((App)Application.Current).CurrentCharacter = (Character)SelectedChar;
            //from json
            Character tempch = ((App)Application.Current).CurrentCharacter;
            ExpandoObject dynamicCh = await OrchidService.GetDynamicCharacter(await OrchidService.GetUserId(tempch), tempch);
            dynamic temp = dynamicCh;
            var temp2 = temp.character;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            ((App)Application.Current).CurrentCharacterProperties = JsonSerializer.Deserialize<ExpandoObject>(temp2, options);

            await ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<CharacterSheetPage>());
        }
        public async Task<List<string>> InitilizeAsync()
        {
            List<Filter> filtersDB = await OrchidService.GetAllFilters();
            List<string> FiltersForApp = new List<string>();
            foreach (Filter filter in filtersDB)
            {
                FiltersForApp.Add(filter.Fname);
            }
            return FiltersForApp;
        }
    }
}
