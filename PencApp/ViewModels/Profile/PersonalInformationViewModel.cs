using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    private User? _currentUser;

    [ObservableProperty] private string? _username;
    
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


    public bool DataHasChanged => FirstName != _currentUser?.FirstName
                                  || LastName != _currentUser?.LastName
                                  || EmailAddress != _currentUser?.EmailAddress
                                  || !DateOfBirth.Equals(_currentUser?.DateOfBirth)
                                  || PhoneNumber != _currentUser?.PhoneNumber;

    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        try
        {
            if (_currentUser is null)
            {
                throw new Exception("No current user");
            }

            Username = _currentUser.Username;
            FirstName = _currentUser.FirstName;
            LastName = _currentUser.LastName;
            EmailAddress = _currentUser.EmailAddress;
            DateOfBirth = _currentUser.DateOfBirth;
            PhoneNumber = _currentUser.PhoneNumber;
        }
        catch (Exception e)
        {
            MainThread.BeginInvokeOnMainThread(async void () =>
            {
                await ExceptionService.HandleExceptionAsync(e, async void () => await GoBackAsync());
            }); 
        }
        
        base.OnNavigatedTo(parameters);
    }

    public override async Task InitializeAsync(INavigationParameters parameters)
    {
        parameters.TryGetValue<User>(nameof(ProfileViewModel.CurrentUser), out _currentUser);
        await base.InitializeAsync(parameters);
    }

    [RelayCommand(CanExecute = nameof(DataHasChanged))]
    private async Task SaveUserAsync()
    {
        dialogService.ShowLoading();
        
        var userData = new User()
        {
            Username = _currentUser!.Username,
            EmailAddress = EmailAddress,
            FirstName = FirstName,
            LastName = LastName
        };

        try
        {
            await userService.SaveUser(userData);
            await userService.UpdateUserDataAsync();
            
            dialogService.HideLoading();
            await dialogService.ShowToast(AppResources.User_saved_success);
            await GoBackAsync();
        }
        catch (Exception e)
        {
            dialogService.HideLoading();
            
            if (e is ApiException apiException)
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiExceptionContent>(apiException.Response);

                if (response?.Detail != null)
                {
                    await dialogService.ShowToast(response.Detail);
                    return;
                }
            }

            await ExceptionService.HandleExceptionAsync(e);
            Debug.WriteLine(e);
        }
    }
}