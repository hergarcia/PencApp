using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PencApp.Controls;
using PencApp.Models;
using PencApp.Resources.Languages;
using PencApp.Services.Exceptions;
using PencApp.Services.User;
using PencApp.ViewModels.Shared;
using PencApp.Views.Home;
using PencApp.Views.Profile;
using PencApp.Views.TabbedPage;
using IDialogService = PencApp.Services.Dialogs.IDialogService;
using User = PencApp.Models.User;

namespace PencApp.ViewModels.Onboarding;

public partial class LoginViewModel(INavigationService navigationService, IExceptionService exceptionService, IDialogService dialogService, IUserService userService)
    : BaseViewModel(navigationService, exceptionService)
{
    private const int PasswordLenght = 8;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
    private EInputState _emailEntryState;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
    private EInputState _passwordEntryState;
    
    [ObservableProperty] private string? _userName;
    [ObservableProperty] private string? _password;
    [ObservableProperty] private string? _emailEntryHelpText;
    [ObservableProperty] private string? _passwordEntryHelpText;

    public bool IsContinueEnabled => /*EmailEntryState == EInputState.Completed &&*/ PasswordEntryState == EInputState.Completed;

    
    [RelayCommand (CanExecute = nameof(IsContinueEnabled))]
    private async Task Continue()
    {
        var loginData = new User()
        {
            Username = "admin",
            Password = "$m8Ab9Srbf6M*94EFo",
            RememberMe = true
        };

        await AuthenticateAndNavigate(loginData);
    }

    private async Task AuthenticateAndNavigate(User loginData)
    {
        dialogService.ShowLoading();
        await userService.LoginAsync(loginData);
        dialogService.HideLoading();
        
        await dialogService.ShowToast(AppResources.Welcome + " " + loginData.Username + "!");
            
        await NavigateToMainTabbedPage();
    }

    private async Task NavigateToMainTabbedPage()
    {
        await NavigationService.NavigateAsync(
            $"{nameof(NavigationPage)}" + "/" + $"{nameof(MainTabbedPage)}" +
            $"?{KnownNavigationParameters.CreateTab}={nameof(HomePage)}" +
            $"&{KnownNavigationParameters.CreateTab}={nameof(ProfilePage)}" +
            $"&{KnownNavigationParameters.SelectedTab}={nameof(ProfilePage)}");
    }

    [RelayCommand]
    private async Task Google()
    {
        var loginData = new User()
        {
            Username = "hergarcia",
            Password = "PJNCcLmrYfDp36v",
            RememberMe = true
        };

        await AuthenticateAndNavigate(loginData);
    }

    [RelayCommand]
    private async Task GoToRegister()
    {
        await NavigationService.CreateBuilder()
            .AddSegment<RegisterViewModel>()
            .NavigateAsync();
    }

    [RelayCommand]
    private async Task GoToForgotPassword()
    {
        await NavigationService.CreateBuilder()
            .AddSegment<SimpleErrorPopupViewModel>(useModalNavigation: true)
            .AddParameter(nameof(SimpleErrorPopupViewModel.SimpleError), new SimpleError()
            {
                Title = "Error",
                Message = "Please try again.",
                Icon = "alert_circle.png",
                ButtonText = "Ok"
            })
            .NavigateAsync();
    }

    // partial void OnEmailChanged(string? value)
    // {
    //     try
    //     {
    //         if (string.IsNullOrEmpty(value))
    //         {
    //             EmailEntryState = EInputState.Default;
    //             EmailEntryHelpText = string.Empty;
    //             return;
    //         }
    //
    //         EmailEntryState = StringHelper.IsEmailValid(value) ? EInputState.Completed : EInputState.Error;
    //
    //         EmailEntryHelpText = EmailEntryState switch
    //         {
    //             EInputState.Default => string.Empty,
    //             EInputState.Error => AppResources.Invalid_email,
    //             EInputState.Completed => string.Empty,
    //             EInputState.Disabled => string.Empty,
    //             _ => throw new ArgumentOutOfRangeException()
    //         };
    //
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.WriteLine(e);
    //         throw;
    //     }
    // }

    partial void OnPasswordChanged(string? value)
    {
        PasswordEntryState = string.IsNullOrEmpty(value) ? EInputState.Default 
            : value.Length < PasswordLenght ? EInputState.Error 
            : EInputState.Completed;

        PasswordEntryHelpText = PasswordEntryState == EInputState.Error
            ? $"Faltan {PasswordLenght - value?.Length} caracteres"
            : string.Empty;
    }
}