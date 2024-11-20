namespace PencApp.Services.Exceptions;

public interface IExceptionService
{
    Task HandleExceptionAsync(Exception exception);
    Task HandleExceptionSilentlyAsync(Exception exception);
    Task HandleRequestServiceErrorAsync(string? content);
}