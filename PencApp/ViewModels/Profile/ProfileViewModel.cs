using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PencApp.Models;
using PencApp.Services.Exceptions;
using PencApp.Services.User;
using PencApp.ViewModels.Onboarding;
using PencApp.Views.Onboarding;

namespace PencApp.ViewModels.Profile;

[ObservableRecipient]
public partial class ProfileViewModel : BaseViewModel, IRecipient<User>
{
    [ObservableProperty]
    private User? _currentUser;

    private readonly IUserService _userService;

    
    public ProfileViewModel(INavigationService navigationService, IExceptionService exceptionService, IUserService userService) : base(navigationService, exceptionService)
    {
        _userService = userService;
        WeakReferenceMessenger.Default.Register(this);
    }
    
    public void Receive(User message)
    {
        CurrentUser = message;
    }
    
    public override async Task InitializeAsync(INavigationParameters parameters)
    {
        try
        {
            CurrentUser = Task.Run(async () => await _userService.GetCurrentUser()).Result;
        }
        catch (Exception e)
        {
            _userService.Logout();
            try
            {

            await NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(LoginPage));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        await base.InitializeAsync(parameters);
    }

    [RelayCommand]
    private async Task GoToPersonalInformation()
    {
        await NavigationService.CreateBuilder()
            .AddSegment<PersonalInformationViewModel>()
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