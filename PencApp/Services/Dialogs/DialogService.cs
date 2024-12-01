using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using PencApp.Controls.Popups;
using PencApp.Helpers;
using PencApp.Resources.Styles;
using Font = Microsoft.Maui.Font;

namespace PencApp.Services.Dialogs;

public class DialogService : IDialogService
{
    private Popup? _loadingPopup;
    private bool _isLastLoadingClosed = true;
    
    public void ShowLoading(string? text = null)
    {
        CloseLoadingPopup();
        
        _loadingPopup = new LoadingPopup(text);
        _loadingPopup.Closed += (_, _) => { _isLastLoadingClosed = true; };
        _loadingPopup.Opened += (_, _) => { _isLastLoadingClosed = false; };

        ShowLoadingPopup();
    }

    public void HideLoading()
    {
        CloseLoadingPopup();
    }
    
    public async Task ShowSnackbar(string message, string actionButtonText, TimeSpan? duration = null, SnackbarOptions? options = null, Action? action = null)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        options ??= new SnackbarOptions
        {
            BackgroundColor = (Color)ApplicationResources.GetResource("PrimaryDarkBlue")!,
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

    private void CloseLoadingPopup()
    {
        if (_isLastLoadingClosed) return;
        MainThread.BeginInvokeOnMainThread(() => _loadingPopup?.Close());
        Debug.WriteLine($"{nameof(DialogService)}.{nameof(CloseLoadingPopup)}");
    }

    private void ShowLoadingPopup()
    {
        if (Application.Current == null || !Application.Current.Windows.Any()) return;
        MainThread.BeginInvokeOnMainThread(() => Application.Current?.Windows[0].Page?.ShowPopup(_loadingPopup!));
        Debug.WriteLine($"{nameof(DialogService)}.{nameof(ShowLoadingPopup)}");
    }
}