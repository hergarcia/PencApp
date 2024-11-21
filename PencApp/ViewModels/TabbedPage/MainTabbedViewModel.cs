using PencApp.Services.Exceptions;

namespace PencApp.ViewModels.TabbedPage;

public class MainTabbedViewModel(INavigationService navigationService, IExceptionService exceptionService)
    : BaseViewModel(navigationService, exceptionService)
{
}