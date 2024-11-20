using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PencApp.Services.ApiClient;
using PencApp.Services.Exceptions;
using PencApp.Services.User;
using User = PencApp.Models.User;

namespace PencApp.ViewModels.Onboarding;

public partial class RegisterViewModel(INavigationService navigationService, IExceptionService exceptionService, IUserService userService)
    : BaseViewModel(navigationService, exceptionService)
{
    [ObservableProperty] private string? _username;
    [ObservableProperty] private string? _emailAddress;
    [ObservableProperty] private string? _password;

    [RelayCommand]
    private async Task Register()
    {
        var registerData = new User()
        {
            Username = Username,
            Password = Password,
            EmailAddress = EmailAddress,
        };
        
        await userService.RegisterAsync(registerData);
    }
}