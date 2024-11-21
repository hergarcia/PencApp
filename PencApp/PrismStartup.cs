using PencApp.Helpers;
using PencApp.Services.ApiClient;
using PencApp.Services.Exceptions;
using PencApp.Services.SecureStorage;
using PencApp.Services.User;
using PencApp.ViewModels.Home;
using PencApp.ViewModels.Onboarding;
using PencApp.ViewModels.Profile;
using PencApp.ViewModels.TabbedPage;
using PencApp.Views.Home;
using PencApp.Views.Onboarding;
using PencApp.Views.Profile;
using PencApp.Views.TabbedPage;
using DialogService = PencApp.Services.Dialogs.DialogService;
using IDialogService = PencApp.Services.Dialogs.IDialogService;

namespace PencApp;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
            .AddGlobalNavigationObserver(context => context.Subscribe(x =>
            {
                if (x.Type == NavigationRequestType.Navigate)
                    Console.WriteLine($"Navigation: {x.Uri}");
                else
                    Console.WriteLine($"Navigation: {x.Type}");

                var status = x.Cancelled ? "Cancelled" : x.Result.Success ? "Success" : "Failed";
                Console.WriteLine($"Result: {status}");

                if (status == "Failed" && !string.IsNullOrEmpty(x.Result?.Exception?.Message))
                    Console.Error.WriteLine(x.Result.Exception.Message);
            }))
            .OnInitialized(VersionTracking.Track)
            .CreateWindow(async (container, navigation) =>
            {
                var userService = container.Resolve<IUserService>();
                var isUSerLoggedIn = await userService.IsUserLoggedIn();
                
                if (isUSerLoggedIn)
                {
                    await navigation.NavigateAsync(
                        $"{nameof(NavigationPage)}" + "/" + $"{nameof(MainTabbedPage)}" +
                        $"?{KnownNavigationParameters.CreateTab}={nameof(HomePage)}" +
                        $"&{KnownNavigationParameters.CreateTab}={nameof(ProfilePage)}" +
                        $"&{KnownNavigationParameters.SelectedTab}={nameof(ProfilePage)}");
                }
                else
                {
                    await navigation.NavigateAsync(nameof(NavigationPage) + "/" + nameof(LoginPage));
                }
            });
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IApiClient, ApiClient>();
        containerRegistry.RegisterSingleton<IExceptionService, ExceptionService>();
        containerRegistry.RegisterSingleton<IUserService, UserService>();
        containerRegistry.RegisterSingleton<ISecureStorageService, SecureStorageService>();
        containerRegistry.RegisterSingleton<IDialogService, DialogService>();
                    
        containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
        containerRegistry.RegisterForNavigation<IntroPage, IntroViewModel>();
        containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>();
        containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedViewModel>();
        containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
        containerRegistry.RegisterForNavigation<ProfilePage, ProfileViewModel>();
        containerRegistry.RegisterForNavigation<PersonalInformationPage, PersonalInformationViewModel>();
    }
}