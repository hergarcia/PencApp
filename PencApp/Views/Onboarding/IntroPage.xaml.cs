using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.LifecycleEvents;
using PencApp.Helpers;

namespace PencApp.Views.Onboarding;

public partial class IntroPage : ContentPage
{
    public IntroPage()
    {
        InitializeComponent();
        Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = (Color)ApplicationResources.GetResource("AppBackgroundColor"),
            StatusBarStyle = StatusBarStyle.DarkContent,
            ApplyOn = StatusBarApplyOn.OnPageNavigatedTo
        });
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        if (CarouselView.Position < CarouselView.ItemsSource.Cast<object>().Count() -1)
            CarouselView.ScrollTo(CarouselView.Position + 1);
    }

}