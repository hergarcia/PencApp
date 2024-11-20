using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using PencApp.Helpers;
using PencApp.Views.Shared;

namespace PencApp.Views.Profile;

public partial class PersonalInformationPage : CustomContentPage
{
    public PersonalInformationPage()
    {
        InitializeComponent();
        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetColor((Color)ApplicationResources.GetResource("AppBackgroundColor")!);
        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetStyle(NavigationBarStyle.DarkContent);
    }
}