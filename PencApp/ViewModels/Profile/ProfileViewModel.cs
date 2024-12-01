using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PencApp.Models;
using PencApp.Services.Exceptions;
using PencApp.Services.User;
using PencApp.ViewModels.Onboarding;

namespace PencApp.ViewModels.Profile;

[ObservableRecipient]
public partial class ProfileViewModel : BaseViewModel, IRecipient<CurrentUserChangedMessage>
{
    [ObservableProperty] private User? _currentUser;
    [ObservableProperty] private string? _nameInitials;
    [ObservableProperty] private bool _loadingUserData = true;

    private readonly IUserService _userService;
    
    public ProfileViewModel(INavigationService navigationService, IExceptionService exceptionService, IUserService userService) : base(navigationService, exceptionService)
    {
        _userService = userService;
        WeakReferenceMessenger.Default.Register(this, IUserService.CurrentUserChangedToken);
    }
    
    public void Receive(CurrentUserChangedMessage message)
    {
        CurrentUser = message.Value;
        SetNameInitials();
        LoadingUserData = false;
    }

    private void SetNameInitials()
    {
        if (CurrentUser is { FirstName: not null, LastName: not null })
        {
            NameInitials = new string(new[] { CurrentUser.FirstName.First(), CurrentUser.LastName.First() }).ToUpper();
        }
    }

    [RelayCommand]
    private async Task GoToPersonalInformation()
    {
        await NavigationService.CreateBuilder()
            .AddSegment<PersonalInformationViewModel>()
            .AddParameter(nameof(CurrentUser), CurrentUser)
            .NavigateAsync();
    }
    
    [RelayCommand]
    private async Task GoToChangePassword()
    {
        await NavigationService.CreateBuilder()
            .AddSegment<ChangePasswordViewModel>()
            .AddParameter(nameof(CurrentUser), CurrentUser)
            .NavigateAsync();
    }

    [RelayCommand]
    private async Task GoToNotifications()
    {
        await NavigationService.CreateBuilder()
            .AddSegment<NotificationsViewModel>()
            .NavigateAsync();
    }

    [RelayCommand]
    private async Task LogOut()
    {
        _userService.Logout();
        
        await NavigationService.CreateBuilder()
            .AddNavigationPage()
            .AddSegment<LoginViewModel>()
            .UseAbsoluteNavigation(true)
            .NavigateAsync();
    }

}