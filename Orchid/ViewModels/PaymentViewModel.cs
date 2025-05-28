using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Orchid.Services;

namespace Orchid.ViewModels
{
    //gets the price from SubscriptionView
    [QueryProperty(nameof(Amount), "price")]
    public class PaymentViewModel : ViewModelBase
    {
        // Service
        private OrchidWebAPIProxy OrchidService;
        // Properties with backing fields
        private string _cardNumber;
        private string _expiryDate;
        private string _cvv;
        private string _cardholderName;
        private string _amount;
        private bool _isProcessing;
        private string _paymentStatus;
        private bool _isPayPalSelected;
        private string _payPalEmail;
        private bool _isCardPaymentSelected = true;

        // Payment methods
        public bool IsCardPaymentSelected
        {
            get => _isCardPaymentSelected;
            set
            {
                if (_isCardPaymentSelected != value)
                {
                    _isCardPaymentSelected = value;
                    OnPropertyChanged();

                    if (value)
                    {
                        IsPayPalSelected = false;
                    }
                }
            }
        }

        public bool IsPayPalSelected
        {
            get => _isPayPalSelected;
            set
            {
                if (_isPayPalSelected != value)
                {
                    _isPayPalSelected = value;
                    OnPropertyChanged();

                    if (value)
                    {
                        IsCardPaymentSelected = false;
                    }
                }
            }
        }

        // Card properties
        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                if (_cardNumber != value)
                {
                    _cardNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ExpiryDate
        {
            get => _expiryDate;
            set
            {
                if (_expiryDate != value)
                {
                    _expiryDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CVV
        {
            get => _cvv;
            set
            {
                if (_cvv != value)
                {
                    _cvv = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CardholderName
        {
            get => _cardholderName;
            set
            {
                if (_cardholderName != value)
                {
                    _cardholderName = value;
                    OnPropertyChanged();
                }
            }
        }

        // PayPal properties
        public string PayPalEmail
        {
            get => _payPalEmail;
            set
            {
                if (_payPalEmail != value)
                {
                    _payPalEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        // Common properties
        public string Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                if (_isProcessing != value)
                {
                    _isProcessing = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PaymentStatus
        {
            get => _paymentStatus;
            set
            {
                if (_paymentStatus != value)
                {
                    _paymentStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        // Commands
        public ICommand ProcessPaymentCommand { get; }
        public ICommand SelectCardPaymentCommand { get; }
        public ICommand SelectPayPalPaymentCommand { get; }
        public ICommand UseTestCardCommand { get; }

        // Constructor
        public PaymentViewModel(OrchidWebAPIProxy proxy)
        {
            //initialize Service
            this.OrchidService = proxy;

            // Initialize default values
            Amount = "0.00";
            PaymentStatus = "Ready";

            // Initialize commands
            ProcessPaymentCommand = new Command(async () => await ProcessPaymentAsync());
            SelectCardPaymentCommand = new Command(() => IsCardPaymentSelected = true);
            SelectPayPalPaymentCommand = new Command(() => IsPayPalSelected = true);
            UseTestCardCommand = new Command(UseTestCard);
        }

        //check if the amount is a valid number
        private async Task ProcessPaymentAsync()
        {
            if (string.IsNullOrWhiteSpace(Amount) || decimal.Parse(Amount) <= 0)
            {
                PaymentStatus = "Invalid amount";
                return;
            }

            IsProcessing = true;
            PaymentStatus = "Processing payment...";

            // Simulate network delay
            await Task.Delay(2000);

            if (IsCardPaymentSelected)
            {
                if (ValidateCardDetails())
                {
                    // Here you would integrate with a real payment gateway
                    PaymentStatus = $"Card payment processed successfully: ${Amount}";
                    updatePremium();
                }
                else
                {
                    PaymentStatus = "Invalid card details";
                }
            }
            else if (IsPayPalSelected)
            {
                if (!string.IsNullOrWhiteSpace(PayPalEmail) && PayPalEmail.Contains("@"))
                {
                    // Here you would integrate with PayPal's API
                    PaymentStatus = $"PayPal payment processed successfully: ${Amount}";
                    updatePremium();
                }
                else
                {
                    PaymentStatus = "Invalid PayPal email";
                }
            }

            IsProcessing = false;
        }


        //update the app user to have a premium subscription
        private async void updatePremium() 
        {
            if (((App)Application.Current).LoggedInUser.IsPremium)
            {
                ((App)Application.Current).LoggedInUser.PremiumUntil = ((App)Application.Current).LoggedInUser.PremiumUntil.AddMonths(1);
            }
            else
            {
                ((App)Application.Current).LoggedInUser.IsPremium = true;
                ((App)Application.Current).LoggedInUser.PremiumUntil = DateTime.Now.AddMonths(1);
            }
            await OrchidService.UpdateAppUser(((App)Application.Current).LoggedInUser);
        }


        //checks and validate the give card information
        private bool ValidateCardDetails()
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(CardNumber) || CardNumber.Length < 13)
                return false;

            if (string.IsNullOrWhiteSpace(ExpiryDate) || !ExpiryDate.Contains("/"))
                return false;

            if (string.IsNullOrWhiteSpace(CVV) || CVV.Length < 3)
                return false;

            if (string.IsNullOrWhiteSpace(CardholderName))
                return false;

            return true;
        }

        // Test card implementation
        private void UseTestCard()
        {
            // Use standard test card details
            CardNumber = "4111 1111 1111 1111"; // Visa test card
            ExpiryDate = "12/29";
            CVV = "123";
            CardholderName = "TEST USER";
            IsCardPaymentSelected = true;
        }

        // INotifyPropertyChanged implementation
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}