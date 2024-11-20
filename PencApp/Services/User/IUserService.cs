using PencApp.Services.ApiClient;

namespace PencApp.Services.User;

public interface IUserService
{
    internal const string IdTokenKey = nameof(IdTokenKey);

    Task<Models.User?> GetCurrentUser(bool refreshIfNull = true);
    Task RegisterAsync(Models.User managedUserVM);
    Task LoginAsync(Models.User loginVm);
    Task<Models.User?> UpdateUserDataAsync();
    Task SaveUser(Models.User adminUserDTO);
    void Logout();
    Task<bool> IsUserLoggedIn();
    Task<string?> GetTokenId();
}