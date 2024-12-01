using CommunityToolkit.Maui.Extensions;
using Maui.BindableProperty.Generator.Core;
using PencApp.Helpers;

namespace PencApp.Controls;

public partial class CustomSwitch : ContentView
{
    // Toggled
    private static readonly Color ToggledBackgroundColor = (Color)ApplicationResources.GetResource("PrimaryDarkBlue");
    private static readonly Color ToggledIndicatorColor = (Color)ApplicationResources.GetResource("PrimaryGreen");
    private const double ToggledIndicatorHeightAndWidth = 24;

    // Not toggled
    private static readonly Color NotToggledBackgroundColor = Colors.Transparent;
    private static readonly Color NotToggledIndicatorColor = Colors.Gray;
    private const double NotToggledIndicatorHeightAndWidth = 16;
    
    public CustomSwitch()
    {
        Loaded += (sender, args) => SetInitialState();
        InitializeComponent();
    }

    #region Bindable Properties
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.TwoWay))] private readonly bool _isToggled;
    #endregion

    partial void OnIsToggledChanged(bool value)
    {
        Task.Run(async () => await Animate(value));
    }

    private async Task Animate(bool isToggled)
    {
        this.CancelAnimations();
        var easing = Easing.CubicInOut;
        const uint AnimationDuration = 500;
        var expandHeightAndWidth = Border.Height - Border.StrokeThickness - 2;
        var indicatorTargetHeightAndWidth = isToggled ? ToggledIndicatorHeightAndWidth : NotToggledIndicatorHeightAndWidth;
        var borderStrokeThicknessTarget = isToggled ? 0 : 2;
        var indicatorTargetPosition = isToggled
            ? CalculateIndicatorTargetPosition(borderStrokeThicknessTarget, ToggledIndicatorHeightAndWidth)
            : CalculateIndicatorTargetPosition(borderStrokeThicknessTarget, NotToggledIndicatorHeightAndWidth) * -1;
        var borderTargetBackgroundColor = isToggled ? ToggledBackgroundColor : NotToggledBackgroundColor;
        var indicatorTargetBackgroundColor = isToggled ? ToggledIndicatorColor : NotToggledIndicatorColor;
        
        Animation toggleAnimation = new()
        {
            { 0, .2, new Animation(x => Ellipse.HeightRequest = x, Ellipse.Height, expandHeightAndWidth) },
            { 0, .2, new Animation(x => Ellipse.WidthRequest = x, Ellipse.Width, expandHeightAndWidth) },
            { 0, .2, new Animation(x => Border.StrokeThickness = x, Border.StrokeThickness, borderStrokeThicknessTarget) },
            
            { .2, 1, new Animation(x => Ellipse.HeightRequest = x, expandHeightAndWidth, indicatorTargetHeightAndWidth) },
            { .2, 1, new Animation(x => Ellipse.WidthRequest = x, expandHeightAndWidth, indicatorTargetHeightAndWidth) },
            { .2, 1, new Animation(x => Ellipse.TranslationX = x, Ellipse.TranslationX, indicatorTargetPosition) },
        };

        await Task.WhenAll(new List<Task>
        {
            Task.Run(() => toggleAnimation.Commit(this, "ToggleAnimation", length: AnimationDuration, easing: easing)),
            Task.Run(async () => await Border.BackgroundColorTo(borderTargetBackgroundColor, length: AnimationDuration / 2, easing: easing)),
            Task.Run(async () => await Ellipse.BackgroundColorTo(indicatorTargetBackgroundColor, length: AnimationDuration / 2, easing: easing)),
        });
    }
    
    private double CalculateIndicatorTargetPosition(double borderStrokeThickness, double indicatorTargetHeighAndWidth)
    {
        return (Border.WidthRequest / 2) - (indicatorTargetHeighAndWidth / 2) - (Border.HeightRequest - borderStrokeThickness - indicatorTargetHeighAndWidth) / 2;
    }
        
    private void SetInitialState()
    {
        if (IsToggled)
        {
            Border.BackgroundColor = ToggledBackgroundColor;
            Border.StrokeThickness = 0;

            Ellipse.HeightRequest = ToggledIndicatorHeightAndWidth;
            Ellipse.WidthRequest = ToggledIndicatorHeightAndWidth;
            Ellipse.BackgroundColor = ToggledIndicatorColor;
            Ellipse.TranslationX = CalculateIndicatorTargetPosition(Border.StrokeThickness, ToggledIndicatorHeightAndWidth);
        }
        else
        {
            Border.BackgroundColor = NotToggledBackgroundColor;
            Border.StrokeThickness = 2;
            
            Ellipse.HeightRequest = NotToggledIndicatorHeightAndWidth;
            Ellipse.WidthRequest = NotToggledIndicatorHeightAndWidth;
            Ellipse.BackgroundColor = NotToggledIndicatorColor;
            Ellipse.TranslationX = CalculateIndicatorTargetPosition(Border.StrokeThickness, NotToggledIndicatorHeightAndWidth) * -1;
        }
    }
    
    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        if(IsEnabled)
            IsToggled = !IsToggled;
    }
}