using PencApp.Helpers;
using PencApp.Services.Exceptions;

namespace PencApp.Services.ApiClient;

public partial class ApiClient : IApiClient
{
    public void InjectToken(string idToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", idToken);
    }
    
    public void RemoveToken()
    {
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
    }

    async partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
    {
        var _exceptionService = ServiceHelper.GetService<IExceptionService>();
        try
        {
            if (response.IsSuccessStatusCode)
            {
            
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                _exceptionService.HandleRequestServiceErrorAsync(content);
            }
        }
        catch (Exception e)
        {
            await _exceptionService.HandleExceptionSilentlyAsync(e);
        }
    } 
}