using System.Windows.Input;
using Maui.BindableProperty.Generator.Core;
using PencApp.Helpers;

namespace PencApp.Controls;

public partial class CustomNavigationBar : ContentView
{    
    private static readonly Color DefaultTextColor = (Color)ApplicationResources.GetResource("DarkGreen");

    public CustomNavigationBar()
    {
        InitializeComponent();
        Content.BindingContext = this;
    }
    
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _title;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly ICommand? _leftButtonCommand;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly ICommand? _rightButtonCommand;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = "arrow_left.png")] private readonly string? _leftButtonImage;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _rightButtonImage;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = "true")] private readonly bool? _hasLeftButton;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = "false")] private readonly bool? _hasRightButton;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _leftButtonBackgroundColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _rightButtonBackgroundColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _leftButtonIconColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _rightButtonIconColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultTextColor))] private readonly Color? _textColor;
}