namespace PencApp.Services.SecureStorage;

public interface ISecureStorageService
{
    Task SetAsync(string key, string value);
    Task<string?> GetAsync(string key);
    bool Clear(string key);
    void ClearAll();
}