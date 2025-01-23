using Microsoft.Extensions.Logging;
using Orchid.Services;
using Orchid.ViewModels;
using Orchid.Views;
using CommunityToolkit.Maui;

namespace Orchid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>().UseMauiCommunityToolkit() // for adding expander in addcontentview
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterDataServices()
                .RegisterPages()
                .RegisterViewModels();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {

            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<SignUpView>();
            builder.Services.AddTransient<AA_classView>();
            builder.Services.AddTransient<AA_equipmentView>();
            builder.Services.AddTransient<AA_featsView>();
            builder.Services.AddTransient<AA_imgGeneratorView>();
            builder.Services.AddTransient<AA_raceView>();
            builder.Services.AddTransient<AA_skillsView>();
            builder.Services.AddTransient<AA_spellsView>();
            builder.Services.AddTransient<AA_statsView>();
            builder.Services.AddTransient<AA_subclassView>();
            builder.Services.AddTransient<AddContentView>();
            builder.Services.AddTransient<AppealView>();
            builder.Services.AddTransient<BrowseView>();
            builder.Services.AddTransient<Ch_ListView>();
            builder.Services.AddTransient<ChView>();
            builder.Services.AddTransient<ForumListView>();
            builder.Services.AddTransient<ForumsView>();
            builder.Services.AddTransient<PartyListView>();
            builder.Services.AddTransient<PartyManagerView>();
            builder.Services.AddTransient<PartyView>();
            builder.Services.AddTransient<PostView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<SubscriptionView>();
            builder.Services.AddTransient<ViewPostView>();








            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<OrchidWebAPIProxy>();
            builder.Services.AddSingleton<ExternalService>();

            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ShellViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddSingleton<AA_classViewModel>();
            builder.Services.AddSingleton<AA_equipmentViewModel>();
            builder.Services.AddSingleton<AA_featsViewModel>();
            builder.Services.AddSingleton<AA_imgGeneratorViewModel>();
            builder.Services.AddSingleton<AA_raceViewModel>();
            builder.Services.AddSingleton<AA_skillsViewModel>();
            builder.Services.AddSingleton<AA_spellsViewModel>();
            builder.Services.AddSingleton<AA_statsViewModel>();
            builder.Services.AddSingleton<AA_subclassViewModel>();
            builder.Services.AddSingleton<AddContentViewModel>();
            builder.Services.AddSingleton<AppealViewModel>();
            builder.Services.AddSingleton<BrowseViewModel>();
            builder.Services.AddSingleton<Ch_ListViewModel>();
            builder.Services.AddSingleton<ChViewModel>();
            builder.Services.AddSingleton<ForumListViewModel>();
            builder.Services.AddSingleton<ForumsViewModel>();
            builder.Services.AddSingleton<PartyListViewModel>();
            builder.Services.AddSingleton<PartyManagerViewModel>();
            builder.Services.AddSingleton<PartyViewModel>();
            builder.Services.AddSingleton<PostViewModel>();
            builder.Services.AddSingleton<ProfileViewModel>();
            builder.Services.AddSingleton<SubscriptionViewModel>();
            builder.Services.AddSingleton<ViewPostViewModel>();
            return builder;
        }
    }
}
