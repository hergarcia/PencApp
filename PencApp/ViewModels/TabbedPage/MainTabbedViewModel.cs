using PencApp.Services.Exceptions;

namespace PencApp.ViewModels.TabbedPage;

public class MainTabbedViewModel(INavigationService navigationService, IExceptionService exceptionService)
    : BaseViewModel(navigationService, exceptionService)
{
    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
    }
}