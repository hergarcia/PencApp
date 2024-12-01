using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using PencApp.Helpers;
using PencApp.ViewModels;

namespace PencApp.Views.Shared;

public class CustomContentPage : ContentPage
{
    public CustomContentPage()
    {
        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetColor((Color)ApplicationResources.GetResource("AppBackgroundColor")!);
        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetStyle(NavigationBarStyle.DarkContent);
    }

    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is not BaseViewModel viewModel) return base.OnBackButtonPressed();
        viewModel.OnAndroidBackButtonPressedAsync();
        return true;

    }

}