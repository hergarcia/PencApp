using System.Diagnostics;
using PencApp.Models;
using PencApp.Services.ApiClient;
using PencApp.ViewModels.Shared;

#if RELEASE
using PencApp.Resources.Languages;
#endif

namespace PencApp.Services.Exceptions;

public class ExceptionService : IExceptionService
{
    public INavigationService NavigationService { get; }

    public ExceptionService(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
    
    public async Task HandleExceptionAsync(Exception exception)
    {
        SimpleError simpleError = new()
        {
            ButtonText = "OK",
            Icon = "alert_circle.png",
        };
        
        if (exception is ApiException apiException)
        {
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiExceptionContent>(apiException.Response);
            
            if (response is not null)
            {
#if DEBUG
                simpleError.Title = "Api Exception";
                simpleError.Message = response
                                      + "\n\n"
                                      + "StackTrace:\n" + apiException.StackTrace;
#else
                simpleError.Title = "\ud83d\ude13" + $"{AppResources.Default_exception_title}";
                simpleError.Message = response.Detail;
#endif
                Debug.WriteLine(response);
            }
        }
        else
        {
#if DEBUG
            simpleError.Title = "Exception";
            simpleError.Message = exception.Message
                                  + "\n\n"
                                  + "StackTrace:\n" + exception.StackTrace;
#else
            simpleError.Title = "\ud83d\ude13" + $"{AppResources.Default_exception_title}";
            simpleError.Message = exception.Message;
#endif
        }
        
        await ShowSimpleErrorAsync(simpleError);
        Debug.WriteLine(exception);
    }

    public async Task HandleExceptionAsync(Exception exception, Action? onException = null)
    {
        await HandleExceptionAsync(exception);
        onException?.Invoke();
    }

    public async Task HandleExceptionSilentlyAsync(Exception exception)
    {
        Debug.WriteLine(exception);
        
        throw exception;
    }

    public Task HandleRequestServiceErrorAsync(ApiExceptionContent content)
    {
        Debug.WriteLine($"[ERROR RESPONSE] - Content: {content}");
        return Task.CompletedTask;
    }
    
    private async Task ShowSimpleErrorAsync(SimpleError simpleError)
    {
        await NavigationService.CreateBuilder()
            .AddSegment<SimpleErrorPopupViewModel>(useModalNavigation: true)
            .AddParameter(nameof(SimpleErrorPopupViewModel.SimpleError), simpleError)
            .NavigateAsync();
    }
}