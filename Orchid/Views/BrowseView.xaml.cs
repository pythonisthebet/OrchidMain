using Orchid.CustomXAMLElements;
using Orchid.ViewModels;
using Orchid.Services;
namespace Orchid.Views;

public partial class BrowseView : ContentPage
{
    private OrchidWebAPIProxy OrchidService;
    private IServiceProvider serviceProvider;


    private WordAutoCompleteEntry _autoCompleteEntry;
    public BrowseView(BrowseViewModel vm, OrchidWebAPIProxy proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.OrchidService = proxy;
        this.BindingContext = vm;
        InitializeComponent();
    }

    //Calls the InitilizeAsync function on page appearence to load data
    protected override async void OnAppearing()
    {
        BrowseViewModel _vm = (BindingContext as BrowseViewModel);
        List<string> _filters = await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
        SetupAutoComplete(_filters);
    }

    //set up the custom XAML element _autoCompleteEntry that auto complete each word individually
    private void SetupAutoComplete(List<string> _filters)
    {
        _autoCompleteEntry = new WordAutoCompleteEntry
        {
            BindingContext = this.BindingContext,
            Placeholder = "Type to search...",
            Suggestions = _filters
        };
        //_autoCompleteEntry.SetBinding(Label.TextProperty, "SelectedFilters", BindingMode.TwoWay);
        _autoCompleteEntry.SetBinding(InputView.TextProperty, "SelectedFilters", BindingMode.TwoWay);


        // Add the container grid (which includes both entry and suggestions list)
        AutoCompleteContainer.Content = _autoCompleteEntry.GetContainerGrid();
    }
}