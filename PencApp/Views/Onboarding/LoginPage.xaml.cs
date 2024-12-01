using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using PencApp.Helpers;
using PencApp.Views.Shared;

namespace PencApp.Views.Onboarding;

public partial class LoginPage : CustomContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        
        Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = (Color)ApplicationResources.GetResource("PrimaryDarkBlue"),
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