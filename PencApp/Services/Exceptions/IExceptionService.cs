using PencApp.Services.ApiClient;

namespace PencApp.Services.Exceptions;

public interface IExceptionService
{
    Task HandleExceptionAsync(Exception exception);
    Task HandleExceptionAsync(Exception exception, Action? onException = null);
    Task HandleExceptionSilentlyAsync(Exception exception);
    Task HandleRequestServiceErrorAsync(ApiExceptionContent content);
}