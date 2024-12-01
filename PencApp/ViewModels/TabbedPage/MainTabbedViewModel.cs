using Microsoft.Maui.Platform;
using PencApp.Helpers;
using PencApp.Services.Exceptions;
using PencApp.Services.User;

namespace PencApp.ViewModels.TabbedPage;

public class MainTabbedViewModel(INavigationService navigationService, IExceptionService exceptionService, IUserService _userService)
    : BaseViewModel(navigationService, exceptionService)
{
    public override void Initialize(INavigationParameters parameters)
    {
        base.Initialize(parameters);
        _userService.UpdateUserData();
    }

    public override void OnAppearing()
    {
#if ANDROID
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Platform.CurrentActivity.Window.SetNavigationBarColor(((Color)ApplicationResources.GetResource("PrimaryDarkBlue")).ToPlatform());
        });
        base.OnAppearing();
#endif
    }
}