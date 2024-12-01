using System.Diagnostics;
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
            try
            {
                var contentString = await response.Content.ReadAsStringAsync();
                var content = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiExceptionContent>(contentString);
                    
                if(content is not null)
                    await _exceptionService!.HandleRequestServiceErrorAsync(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    } 
}