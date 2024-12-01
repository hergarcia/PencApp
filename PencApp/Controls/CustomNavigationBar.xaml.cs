using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Behaviors;
using Maui.BindableProperty.Generator.Core;
using PencApp.Helpers;

namespace PencApp.Controls;

public partial class CustomNavigationBar : ContentView
{    
    private static readonly Color DefaultButtonBorderColor = (Color)ApplicationResources.GetResource("LightGray");

    public CustomNavigationBar()
    {
        InitializeComponent();
        Content.BindingContext = this;
    }
    
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _title;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _subTitle;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly ICommand? _leftButtonCommand;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly ICommand? _rightButtonCommand;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = "arrow_left.png")] private readonly string? _leftButtonImage;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _rightButtonImage;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = "true")] private readonly bool _hasLeftButton;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = "false")] private readonly bool _hasRightButton;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _leftButtonBackgroundColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _rightButtonBackgroundColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultButtonBorderColor))] private readonly Color? _leftButtonBorderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultButtonBorderColor))] private readonly Color? _rightButtonBorderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _leftButtonIconColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _rightButtonIconColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = "Colors.White")] private readonly Color? _textColor;

    private void RightButton_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is ImageButton rightImageButton)
        {
            if (e.PropertyName == nameof(IsEnabled))
            {
                HandleButtonIsEnabled(rightImageButton);
            }
        }
    }

    private void LeftButton_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is ImageButton leftImageButton)
        {
            if (e.PropertyName == nameof(IsEnabled))
            {
                HandleButtonIsEnabled(leftImageButton);
            }
        }
    }
    
    
    private void HandleButtonIsEnabled(ImageButton imageButton)
    {
        imageButton.Behaviors.Clear();

        if (imageButton.IsEnabled)
        {
            imageButton.Behaviors.Add(new IconTintColorBehavior()
            {
                TintColor = RightButtonIconColor,
            });
        }
        else
        {
            imageButton.Behaviors.Add(new IconTintColorBehavior()
            {
                TintColor = RightButtonBorderColor,
            });
        }
    }


}