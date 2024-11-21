using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using FFImageLoading.Maui;
using Microsoft.Maui.Controls.PlatformConfiguration;
using PencApp.Controls.Popups;
using PencApp.Helpers;
using PencApp.Resources.Styles;
using Font = Microsoft.Maui.Font;

namespace PencApp.Services.Dialogs;

public class DialogService : IDialogService
{
    private Popup? _loadingPopup;
    private bool _isLastLoadingClosed;
    
    public void ShowLoading(string? text = null)
    {
        if(!_isLastLoadingClosed) _loadingPopup?.Close();
        
        _loadingPopup = new LoadingPopup(text);
        _loadingPopup.Closed += (_, _) => { _isLastLoadingClosed = true; };
        _loadingPopup.Opened += (_, _) => { _isLastLoadingClosed = false; };
        
        if(Application.Current != null && Application.Current.Windows.Any())
           Application.Current?.Windows[0].Page?.ShowPopup(_loadingPopup);
    }

    public void HideLoading()
    {
        _loadingPopup?.Close();
    }
    
    public async Task ShowSnackbar(string message, string actionButtonText, TimeSpan? duration = null, SnackbarOptions? options = null, Action? action = null)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        options ??= new SnackbarOptions
        {
            BackgroundColor = (Color)ApplicationResources.GetResource("BlueHeader"),
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.Gray,
            CornerRadius = UiConstants.DefaultCornerRadius,
            Font = Font.SystemFontOfSize(14),
            ActionButtonFont = Font.SystemFontOfSize(14),
        };
        
        duration ??= TimeSpan.FromSeconds(3);

        var snackbar = Snackbar.Make(message, action, actionButtonText, duration, options);

        await snackbar.Show(cancellationTokenSource.Token);
    }

    public async Task ShowToast(string message, ToastDuration duration = ToastDuration.Short)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        var toast = Toast.Make(message, duration, 14);

        await toast.Show(cancellationTokenSource.Token);
    }
}