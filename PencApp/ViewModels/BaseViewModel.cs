using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.LifecycleEvents;
using PencApp.Helpers;
using PencApp.Services.Exceptions;
using INavigationParameters = Prism.Navigation.INavigationParameters;


namespace PencApp.ViewModels;
public partial class BaseViewModel : ObservableObject, IDestructible, IApplicationLifecycleAware, IPageLifecycleAware, INavigationAware, IInitialize, IInitializeAsync, INavigationPageOptions
{
    public const string IsBackNavigation = nameof(IsBackNavigation);

    public INavigationService NavigationService { get; }
    public IExceptionService ExceptionService { get; }

    public virtual bool ClearNavigationStackOnNavigation => true;

    public BaseViewModel(INavigationService navigationService, IExceptionService exceptionService)
    {
        NavigationService = navigationService;
        ExceptionService = exceptionService;
    }

    public virtual void OnResume()
    {
    }

    public virtual void OnSleep()
    {

    }

    public virtual void OnAppearing()
    {

    }

    public virtual void OnDisappearing()
    {

    }

    public virtual void OnNavigatedFrom(INavigationParameters parameters)
    {

    }

    public virtual void OnNavigatedTo(INavigationParameters parameters)
    {

    }

    public virtual void Initialize(INavigationParameters parameters)
    {

    }
    
    public virtual Task InitializeAsync(INavigationParameters parameters)
    {
        return Task.CompletedTask;
    }
    
    public void Destroy()
    {
        
    }
    
    public virtual async Task OnAndroidBackButtonPressed() => await GoBackAsync();

    public async Task<INavigationResult> GoBackAsync()
    {
        var isModal = PageHelper.IsCurrentPageModal();

        var navResult = await NavigateGoBack(isModal);
        if (!navResult.Success)
            navResult = await NavigateGoBack(!isModal);

        return navResult;
    }

    private async Task<INavigationResult> NavigateGoBack(bool isModal = false)
    {
        var navParams = new NavigationParameters
        {
            { KnownNavigationParameters.UseModalNavigation, isModal },
            { IsBackNavigation, true}
        };
        
        return await NavigationService.GoBackAsync(navParams);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await GoBackAsync();
    }
}