namespace PencApp.Services.ApiClient;

public partial interface IApiClient
{
    void InjectToken(string idToken);
    void RemoveToken();
}