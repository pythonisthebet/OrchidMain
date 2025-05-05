using Microsoft.Extensions.DependencyInjection;
using Orchid.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.ViewModels
{
    public class SubscriptionViewModel : ViewModelBase
    {
        private IServiceProvider serviceProvider;
        public SubscriptionViewModel(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
            this.PayCommand = new Command<string>(GoToPayment);
        }
        public Command PayCommand { protected set; get; }

        private async void GoToPayment(string price)
        {
            var navParam = new Dictionary<string, object>()
                {
                    { "price" , price }
                };
            await Shell.Current.GoToAsync($"Payment", navParam);
        }
    }
}
