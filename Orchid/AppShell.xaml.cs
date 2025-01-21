using Orchid.Views;
using System.Windows.Input;
using System;
using Orchid.ViewModels;

namespace Orchid
{
    public partial class AppShell : Shell
    {
        public AppShell(ShellViewModel vm)
        {

            InitializeComponent();
            this.BindingContext = vm;
            RegisterRoutes();
        }

        //all the Routing to different pages (via shell) like from QuestionList to QuestionDetailsView 
        private void RegisterRoutes()
        {
            Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
            Routing.RegisterRoute("Ch_List", typeof(Ch_ListView));
            Routing.RegisterRoute("Profile", typeof(ProfileView));
            Routing.RegisterRoute("cheracter", typeof(ChView));
            //Routing.RegisterRoute("AA_classView", typeof(AA_classView));
            Routing.RegisterRoute("forum", typeof(ForumsView));
            Routing.RegisterRoute("Party", typeof(PartyView));
            Routing.RegisterRoute("Post", typeof(PostView));
            Routing.RegisterRoute("ViewPost", typeof(ViewPostView));

        }
    }
}
