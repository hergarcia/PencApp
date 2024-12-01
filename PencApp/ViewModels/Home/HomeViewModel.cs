using CommunityToolkit.Mvvm.ComponentModel;
using PencApp.Services.ApiClient;
using PencApp.Services.Exceptions;
using PencApp.Services.Pencas;

namespace PencApp.ViewModels.Home;

public partial class HomeViewModel(INavigationService navigationService, IExceptionService exceptionService, IPencaService pencaService)
    : BaseViewModel(navigationService, exceptionService)
{
    [ObservableProperty] private List<MatchDTO> _matches;
    
    public async override Task InitializeAsync(INavigationParameters parameters)
    {
        _ = Task.Run(GetMatches);
        
        await base.InitializeAsync(parameters);
    }

    private async Task GetMatches()
    {
        await MainThread.InvokeOnMainThreadAsync(async () => Matches = await pencaService.GetPencaMatches(6101));
    }
}