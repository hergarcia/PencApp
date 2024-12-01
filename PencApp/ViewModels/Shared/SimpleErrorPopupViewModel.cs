using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PencApp.Models;
using PencApp.Services.Exceptions;

namespace PencApp.ViewModels.Shared;

public partial class SimpleErrorPopupViewModel(INavigationService navigationService, IExceptionService exceptionService)
    : BaseViewModel(navigationService, exceptionService)
{
    [ObservableProperty] private SimpleError? _simpleError;
    
    public override void Initialize(INavigationParameters parameters)
    {
        SimpleError = parameters.GetValue<SimpleError>(nameof(SimpleError));
        base.Initialize(parameters);
    }

    [RelayCommand]
    private async Task ButtonCommand()
    {
        if (SimpleError?.ButtonCommand is not null)
        {
            SimpleError.ButtonCommand.Execute(null);
        }
        else
        {
            await GoBackAsync();
        }
    }

    // Cancel Android back button behavior
    public override Task OnAndroidBackButtonPressedAsync()
    {
        return base.OnAndroidBackButtonPressedAsync();
    }
}