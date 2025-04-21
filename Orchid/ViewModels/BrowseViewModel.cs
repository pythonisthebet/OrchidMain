using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Orchid.Models;
using Orchid.Services;

namespace Orchid.ViewModels
{
    public class BrowseViewModel : ViewModelBase
    {
        private IServiceProvider serviceProvider;
        private OrchidWebAPIProxy OrchidService;



        public BrowseViewModel(OrchidWebAPIProxy proxy, ExternalService proxy2, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.OrchidService = proxy;
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
