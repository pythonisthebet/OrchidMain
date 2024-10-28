﻿using Orchid.Models;
using Orchid.Views;
using Orchid.ViewModels;


namespace Orchid
{
    public partial class App : Application
    {

        //Use this class to store global application data that should be accessible throughout the entire app!

        //this is the current user that is logged in
        public AppUser LoggedInUser { get; set; }

        //this is the Login page we have to create one here to not cause a loop couse login => shell == > login if we create a login on logout and not now
        public LoginView Login;


        //initilizing logged in user login page (via builder) and the main page which is the page we show to the user
        public App(LoginView v)
        {
            LoggedInUser = null;
            InitializeComponent();
            Login = v;


            MainPage = new NavigationPage(v);
        }
    }
}
