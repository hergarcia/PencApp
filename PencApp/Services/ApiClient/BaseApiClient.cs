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
        
        if (response.IsSuccessStatusCode)
        {
            
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync();
            if(!string.IsNullOrEmpty(content))
                await _exceptionService!.HandleRequestServiceErrorAsync(content);
        }
    } 
}