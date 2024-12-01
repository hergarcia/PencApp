using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PencApp.Services.ApiClient;
using PencApp.Services.SecureStorage;

namespace PencApp.Services.User;

public partial class UserService(IApiClient apiClient,  ISecureStorageService secureStorageService) : ObservableRecipient, IUserService
{
    [ObservableProperty, NotifyPropertyChangedRecipients]
    private Models.User? _currentUser;

    protected override void Broadcast<T>(T oldValue, T newValue, string? propertyName)
    {
        CurrentUserChangedMessage message = new(CurrentUser);
        WeakReferenceMessenger.Default.Send(message, IUserService.CurrentUserChangedToken);
    }

    public async Task<Models.User?> GetCurrentUser(bool refreshIfNull = true)
    {
        if (CurrentUser is null && refreshIfNull)
            return await UpdateUserDataAsync();

        return CurrentUser;
    }
    
    public async Task LoginAsync(Models.User user)
    {
        var token = await apiClient.AuthorizeAsync(user.ToLoginVM());
        
        if (!string.IsNullOrEmpty(token.Id_token))
        {
            await secureStorageService.SetAsync(IUserService.IdTokenKey, token.Id_token);
            
            apiClient.InjectToken(token.Id_token);
        }
    }

    public async Task RegisterAsync(Models.User user)
    {
        await apiClient.RegisterAccountAsync(user.ToManagedUserVM());
    }

    public void UpdateUserData()
    {
        Task.Run(async () => await UpdateUserDataAsync());
    }
    
    public async Task<Models.User?> UpdateUserDataAsync()
    {
        var response = await apiClient.GetAccountAsync();
        CurrentUser = response?.ToUser();
        return CurrentUser;
    }
    
    public async Task SaveUser(Models.User user)
    {
        await apiClient.SaveAccountAsync(user.ToAdminUserDTO());
    }

    public void Logout()
    {
        apiClient.RemoveToken();
        secureStorageService.Clear(IUserService.IdTokenKey);
        CurrentUser = null;
    }

    public async Task<bool> IsUserLoggedIn()
    {
        var idToken = await GetTokenId();

        if (string.IsNullOrEmpty(idToken))
        {
            apiClient.RemoveToken();
            return false;
        }

        apiClient.InjectToken(idToken);
        return true;
    }

    public async Task<string?> GetTokenId()
    {
        var idToken = await secureStorageService.GetAsync(IUserService.IdTokenKey);
        return idToken;
    }

    public async Task ChangeUserPassword(PasswordChangeDTO passwordChangeDTO)
    {
        await apiClient.ChangePasswordAsync(passwordChangeDTO);
    }
}