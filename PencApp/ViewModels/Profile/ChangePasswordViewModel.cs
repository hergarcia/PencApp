using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PencApp.Resources.Languages;
using PencApp.Services.ApiClient;
using PencApp.Services.Exceptions;
using PencApp.Services.User;
using IDialogService = PencApp.Services.Dialogs.IDialogService;
using User = PencApp.Models.User;

namespace PencApp.ViewModels.Profile;

public partial class ChangePasswordViewModel(INavigationService navigationService, IExceptionService exceptionService, IUserService userService, IDialogService dialogService)
    : BaseViewModel(navigationService, exceptionService)
{
    private User? _currentUser;
    
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(ChangePasswordCommand))] private string? _oldPassword;
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(ChangePasswordCommand))] private string? _newPassword;
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(ChangePasswordCommand))] private string? _newPasswordRepeat;
    
    private bool CanChangePassword =>
        !string.IsNullOrEmpty(OldPassword) &&
        !string.IsNullOrEmpty(NewPassword) &&
        !string.IsNullOrEmpty(NewPasswordRepeat);
    public override async Task InitializeAsync(INavigationParameters parameters)
    {
        PageTitle = AppResources.Change_password;
        parameters.TryGetValue<User>(nameof(ProfileViewModel.CurrentUser), out _currentUser);
        await base.InitializeAsync(parameters);
    }

    [RelayCommand(CanExecute = nameof(CanChangePassword))]
    private async Task ChangePassword()
    {
        try
        {
            if (NewPassword != NewPasswordRepeat)
            {
                throw new Exception("Passwords do not match");
            }
            
            dialogService.ShowLoading();
            var passwordChangeBody = new PasswordChangeDTO()
            {
                CurrentPassword = OldPassword,
                NewPassword = NewPassword
            };

            await userService.ChangeUserPassword(passwordChangeBody);

            dialogService.HideLoading();
            await dialogService.ShowToast("Password changed successfully");
            await GoBackAsync();
        }
        catch (Exception e)
        {
            dialogService.HideLoading();
            await ExceptionService.HandleExceptionAsync(e);
        }
    }
}