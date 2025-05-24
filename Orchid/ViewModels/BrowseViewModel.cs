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

        public List<Character> characterList;

        public List<Character> CharacterList
        {
            get
            {
                return this.characterList;
            }
            set
            {
                this.characterList = value;
                OnPropertyChanged("CharacterList");
            }
        }

        #endregion

        #region constractor
        public BrowseViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.OrchidService = proxy;
            this.CharacterList = new List<Character>();
        }
        #endregion
        public ICommand Search => new Command(OnSearch);

        public ICommand Select => new Command(OnSelect);

        public async void OnSearch()
        {
            InServerCall = true;
            //from db
            ((App)Application.Current).CurrentCharacter = (Character)SelectedChar;
            //from json
            this.CharacterList.Clear();
            List<Filter> filters = await OrchidService.GetAllFilters();
            List<Filter> filtered = new List<Filter>(filters);

            if (SelectedFilters == null)
            {
                filters.Clear();
            }
            else
            {
                foreach (Filter filter in filters)
                {
                    if (!SelectedFilters.Contains(filter.Fname))
                    {
                        filtered.Remove(filter);
                    }
                }
            }
            this.CharacterList = await OrchidService.GetCharactersFORFilters(filtered);
            InServerCall = false;
        }
        public async void OnSelect()
        {
            InServerCall = true;
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
            InServerCall = false;
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
