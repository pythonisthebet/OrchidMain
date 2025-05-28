using Microsoft.Extensions.Logging;
using Orchid.Services;
using Orchid.ViewModels;
using Orchid.Views;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;

namespace Orchid
{
    public static class MauiProgram
    {
        //builds the app (allow for pages to appear without calling for their constractor)
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

        //register all views in the builder
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
            builder.Services.AddTransient<AA_filtersView>();
            builder.Services.AddTransient<AppealView>();
            builder.Services.AddTransient<BrowseView>();
            builder.Services.AddTransient<Ch_ListView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<SubscriptionView>();

            builder.Services.AddTransient<CharacterSheetPage>();

            builder.Services.AddTransient<PaymentPage>();

            builder.Services.AddTransient<AppealListAView>();
            builder.Services.AddTransient<UserListAView>();
            builder.Services.AddTransient<CharacterListAView>();






            return builder;
        }

        //register all services in the builder
        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<OrchidWebAPIProxy>();
            builder.Services.AddSingleton<ExternalService>();
            builder.Services.AddSingleton<PdfService>();
            builder.Services.AddSingleton<CharacterService>();
            builder.Services.AddSingleton<Orchid.Services.FileSaverImplementation>();
            return builder;
        }

        //register all view models in the builder
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
            builder.Services.AddSingleton<AA_filtersViewModel>();
            builder.Services.AddSingleton<AppealViewModel>();
            builder.Services.AddSingleton<BrowseViewModel>();
            builder.Services.AddSingleton<Ch_ListViewModel>();
            builder.Services.AddSingleton<ProfileViewModel>();
            builder.Services.AddSingleton<SubscriptionViewModel>();

            builder.Services.AddSingleton<PaymentViewModel>();

            builder.Services.AddSingleton<CharacterSheetViewModel>();

            builder.Services.AddSingleton<AppealListAViewModel>();
            builder.Services.AddSingleton<UserListAViewModel>();
            builder.Services.AddSingleton<CharacterListAViewModel>();



            return builder;
        }
    }
}
