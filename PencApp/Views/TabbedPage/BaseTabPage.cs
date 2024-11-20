using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using PencApp.Helpers;
using PencApp.Views.Shared;

namespace PencApp.Views.TabbedPage;

public class BaseTabPage : CustomContentPage
{
    public BaseTabPage()
    {
        Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = (Color)ApplicationResources.GetResource("BlueHeader"),
            StatusBarStyle = StatusBarStyle.LightContent,
            ApplyOn = StatusBarApplyOn.OnPageNavigatedTo
        });
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetColor((Color)ApplicationResources.GetResource("BlueHeader"));
            On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetStyle(NavigationBarStyle.LightContent);
        });
    }
}