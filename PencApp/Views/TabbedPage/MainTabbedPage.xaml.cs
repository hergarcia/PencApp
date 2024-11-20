using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using PencApp.Helpers;

namespace PencApp.Views.TabbedPage;

public partial class MainTabbedPage : Microsoft.Maui.Controls.TabbedPage
{
    public MainTabbedPage()
    {
        InitializeComponent();
        
        Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = (Color)ApplicationResources.GetResource("BlueHeader"),
            StatusBarStyle = StatusBarStyle.LightContent,
            ApplyOn = StatusBarApplyOn.OnPageNavigatedTo
        });
        
        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
    }
}