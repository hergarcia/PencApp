using PencApp.Services.Exceptions;

namespace PencApp.ViewModels.Home;

public class HomeViewModel(INavigationService navigationService, IExceptionService exceptionService)
    : BaseViewModel(navigationService, exceptionService)
{
}