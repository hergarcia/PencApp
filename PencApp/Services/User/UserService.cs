using CommunityToolkit.Mvvm.Messaging;
using PencApp.Services.ApiClient;
using PencApp.Services.SecureStorage;

namespace PencApp.Services.User;

public class UserService(IApiClient apiClient, ISecureStorageService secureStorageService) : IUserService
{
    private static Models.User? _currentUser;
    
    public async Task<Models.User?> GetCurrentUser(bool refreshIfNull = true)
    {
        if (_currentUser is null && refreshIfNull)
            return await UpdateUserDataAsync();
        
        return _currentUser;
    }
    
    public async Task LoginAsync(Models.User user)
    {
        var token = await apiClient.AuthenticatePostAsync(user.ToLoginVM());
        
        if (!string.IsNullOrEmpty(token.Id_token))
        {
            await secureStorageService.SetAsync(IUserService.IdTokenKey, token.Id_token);
            
            apiClient.InjectToken(token.Id_token);
        }
    }

    public async Task RegisterAsync(Models.User user)
    {
        await apiClient.RegisterAsync(user.ToManagedUserVM());
    }

    public async Task<Models.User?> UpdateUserDataAsync()
    {
        var response = await apiClient.AccountGetAsync();
        _currentUser = response?.ToUser();
        WeakReferenceMessenger.Default.Send(_currentUser);
        return _currentUser;
    }

    public async Task SaveUser(Models.User user)
    {
        await apiClient.AccountPostAsync(user.ToAdminUserDTO());
    }

    public void Logout()
    {
        apiClient.RemoveToken();
        secureStorageService.Clear(IUserService.IdTokenKey);
        _currentUser = null;
    }

    public async Task<bool> IsUserLoggedIn()
    {
        return !string.IsNullOrEmpty(await GetTokenId());
    }

    public async Task<string?> GetTokenId()
    {
        var idToken = await secureStorageService.GetAsync(IUserService.IdTokenKey);
        return idToken;
    }
}