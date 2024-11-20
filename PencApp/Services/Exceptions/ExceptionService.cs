using PencApp.Services.ApiClient;

namespace PencApp.Services.Exceptions;

public class ExceptionService :IExceptionService
{
    public async Task HandleExceptionAsync(Exception exception)
    {
        Console.WriteLine(exception);

        if (exception is ApiException apiException)
        {
            await Application.Current.Windows[0].Page.DisplayAlert("Api Exception", apiException.Message 
                + "\n\n"
                + "StackTrace:\n" + apiException.StackTrace, 
                "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Exception", exception.Message, "OK");
        }
    }

    public async Task HandleExceptionSilentlyAsync(Exception exception)
    {
        Console.WriteLine(exception);
        
        throw exception;
    }

    public Task HandleRequestServiceErrorAsync(string? content)
    {
        Console.WriteLine($"[ERROR RESPONSE] - Content: {content}");
        return Task.CompletedTask;
    }
}