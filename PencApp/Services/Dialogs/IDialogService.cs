using CommunityToolkit.Maui.Core;

namespace PencApp.Services.Dialogs;

public interface IDialogService
{
    void ShowLoading(string? text = null);
    Task HideLoading();
    Task ShowSnackbar(string message, string actionButtonText, TimeSpan? duration = null,
        SnackbarOptions? options = null, Action? action = null);

    Task ShowToast(string message, ToastDuration duration = ToastDuration.Short);
}