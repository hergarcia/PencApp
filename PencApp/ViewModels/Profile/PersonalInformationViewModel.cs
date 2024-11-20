using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;
using PencApp.Helpers;
using PencApp.Services.ApiClient;
using PencApp.Services.Exceptions;
using PencApp.Services.User;
using IDialogService = PencApp.Services.Dialogs.IDialogService;
using User = PencApp.Models.User;

namespace PencApp.ViewModels.Profile;

public partial class PersonalInformationViewModel(
    INavigationService navigationService,
    IExceptionService exceptionService,
    IUserService userService,
    IDialogService dialogService)
    : BaseViewModel(navigationService, exceptionService)
{
    [ObservableProperty] private User _currentUser;
    
    public override async Task InitializeAsync(INavigationParameters parameters)
    {
        CurrentUser = await userService.GetCurrentUser();
        await base.InitializeAsync(parameters);
    }

    [RelayCommand]
    private async Task SaveUserAsync()
    {
        dialogService.ShowLoading();
        
        var userData = new User()
        {
            Username = CurrentUser.Username,
            EmailAddress = CurrentUser.EmailAddress,
            FirstName = CurrentUser.FirstName,
            LastName = CurrentUser.LastName
        };

        try
        {
            await userService.SaveUser(userData);
            await userService.UpdateUserDataAsync();
        }
        catch (Exception e)
        {
            await dialogService.ShowToast(e.Message);
            Console.WriteLine(e);
            // throw;
        }
        finally
        {
            dialogService.HideLoading();
            await dialogService.ShowToast("Usuario guardado con éxito");
            await GoBackAsync();
        }
        
    }

    public override void OnNavigatedFrom(INavigationParameters parameters)
    {
        base.OnNavigatedFrom(parameters);
#if ANDROID
        var isBackNavigation = parameters.GetValue<bool>(IsBackNavigation);
        if (isBackNavigation)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Platform.CurrentActivity.Window.SetNavigationBarColor(((Color)ApplicationResources.GetResource("BlueHeader")).ToPlatform());
            });
        }
#endif
    }
}