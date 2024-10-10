﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Orchid.Models;

namespace Orchid.ViewModels
{
    public class ShellViewModel
    {
        //this page is just for a log out command and not showing views if you dont have permission 

        //constractor
        //initilizing the logout command
        public ShellViewModel() 
        {
            this.LogoutCommand = new Command(OnLogout);
        }

        //on pressing logout in the shell bar on the left
        public ICommand LogoutCommand { get; set; }


        #region attributes and paramaters
        //private bool isMaster; from trivia clean
        private bool isAdmin;
        //public bool IsMaster from trivia clean
        //{
        //    get
        //    {
        //        if ((App)Application.Current == null)
        //            return true;
        //        else if (((App)Application.Current).LoggedInUser.Rank > 0)
        //            return true; 
        //        else
        //            return false;
        //    }
        //}
        public bool IsAdmin
        {
            get
            {
                if ((App)Application.Current == null)
                    return true;
                else if (((App)Application.Current).LoggedInUser.IsAdmin == true)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        //on LogoutCommand
        //clear the current user and send them to the login screen exiting the shell
        public async void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;
            ((App)Application.Current).MainPage = ((App)Application.Current).Login;
        }
    }
}
