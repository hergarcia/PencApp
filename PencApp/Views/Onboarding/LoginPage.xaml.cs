using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using PencApp.Helpers;

namespace PencApp.Views.Onboarding;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        
        Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = (Color)ApplicationResources.GetResource("BlueHeader"),
            StatusBarStyle = StatusBarStyle.LightContent,
            ApplyOn = StatusBarApplyOn.OnPageNavigatedTo
        });
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetColor((Color)ApplicationResources.GetResource("AppBackgroundColor"));
            On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetStyle(NavigationBarStyle.DarkContent);
        });
    }
}