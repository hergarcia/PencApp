namespace PencApp.Helpers;

public static class PlatformHelper
{
    // public static async Task SetNavigationBarColor(Color color)
    // {
    //     if (DeviceInfo.Current.Platform == DevicePlatform.Android)
    //     {
    //         if (Microsoft.Maui.ApplicationModel.Platform.CurrentActivity == null)
    //             await Platform.WaitForActivityAsync();
    //
    //         MainThread.BeginInvokeOnMainThread(() => Microsoft.Maui.ApplicationModel.Platform.CurrentActivity!.Window?.SetNavigationBarColor(color.ToPlatform()));
    //     }
    // }
    //
    // public static void SetAppTheme(AppTheme appTheme)
    // {
    //     if (DeviceInfo.Current.Platform == DevicePlatform.Android)
    //     {
    //         MainThread.BeginInvokeOnMainThread(() => Application.Current!.UserAppTheme = appTheme);
    //
    //         // Settings.IsLightMode = appTheme == AppTheme.Light;
    //     }
    // }
    //
    // public static void SetStatusBarColor(Color color)
    // {
    //     MainThread.BeginInvokeOnMainThread(() =>
    //     {
    //         if (DeviceInfo.Current.Platform != DevicePlatform.Android ||
    //             !OperatingSystem.IsAndroidVersionAtLeast(23)) return;
    //         CommunityToolkit.Maui.Core.Platform.StatusBar.SetColor(color);
    //     });
    // }
    //
    // public static void SetStatusBarStyle(StatusBarStyle statusBarStyle)
    // {
    //     if(!OperatingSystem.IsAndroidVersionAtLeast(23)) return;
    //     CommunityToolkit.Maui.Core.Platform.StatusBar.SetStyle(statusBarStyle);
    // }
}