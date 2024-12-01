using PencApp.Services.Exceptions;

namespace PencApp.ViewModels.Profile;

public class NotificationsViewModel(INavigationService navigationService, IExceptionService exceptionService)
    : BaseViewModel(navigationService, exceptionService);