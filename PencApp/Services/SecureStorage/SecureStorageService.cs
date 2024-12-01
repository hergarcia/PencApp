using System.Diagnostics;

namespace PencApp.Services.SecureStorage;

public class SecureStorageService : ISecureStorageService
{
    private ISecureStorage _secureStorage;
    
    public SecureStorageService()
    {
        _secureStorage = Microsoft.Maui.Storage.SecureStorage.Default;
    }
    
    public async Task SetAsync(string key, string value)
    {
        try
        {
            await _secureStorage.SetAsync(key, value);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }

    public async Task<string?> GetAsync(string key)
    {
        try
        {
            var result = Task.Run(async () => await _secureStorage.GetAsync(key)).Result;  
            return result;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }

    public bool Clear(string key)
    {
        try
        {
            return _secureStorage.Remove(key);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }

    public void ClearAll()
    {
        try
        {
            _secureStorage.RemoveAll();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }
}