using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;
using PencApp.Helpers;
using PencApp.Resources.Languages;
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
    [ObservableProperty]
    private User? _currentUser;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
    private string? _firstName;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
    private string? _lastName;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
    private string? _emailAddress;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
    private DateTime _dateOfBirth;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
    private string? _phoneNumber;


    public bool DataHasChanged => FirstName != CurrentUser?.FirstName || LastName != CurrentUser?.LastName ||
                                  EmailAddress != CurrentUser?.EmailAddress ||
                                  !DateOfBirth.Equals(CurrentUser?.DateOfBirth) || PhoneNumber != CurrentUser?.PhoneNumber;
    
    public override async Task InitializeAsync(INavigationParameters parameters)
    {
        try
        {
            CurrentUser = await userService.GetCurrentUser();
            
            if (CurrentUser is not null)
            {
                FirstName = CurrentUser.FirstName;
                LastName = CurrentUser.LastName;
                EmailAddress = CurrentUser.EmailAddress;
                DateOfBirth = CurrentUser.DateOfBirth;
                PhoneNumber = CurrentUser.PhoneNumber;
            }
        }
        catch (Exception e)
        {
            await ExceptionService.HandleExceptionAsync(e);
            await GoBackAsync();
        }
        await base.InitializeAsync(parameters);
    }

    [RelayCommand(CanExecute = nameof(DataHasChanged))]
    private async Task SaveUserAsync()
    {
        dialogService.ShowLoading();
        
        var userData = new User()
        {
            Username = CurrentUser!.Username,
            EmailAddress = EmailAddress,
            FirstName = FirstName,
            LastName = LastName
        };

        try
        {
            await userService.SaveUser(userData);
            await userService.UpdateUserDataAsync();
            await dialogService.ShowToast(AppResources.User_saved_success);
            await GoBackAsync();
        }
        catch (Exception e)
        {
            await dialogService.ShowToast(e.Message);
            Console.WriteLine(e);
        }
        finally
        {
            dialogService.HideLoading();
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