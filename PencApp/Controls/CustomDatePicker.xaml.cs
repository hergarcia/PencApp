using System.ComponentModel;
using System.Windows.Input;
using Maui.BindableProperty.Generator.Core;
using PencApp.Helpers;
using PencApp.Resources.Languages;

namespace PencApp.Controls;

public partial class CustomDatePicker : ContentView, IInputBase
{
    private static readonly Color DefaultTextColor = (Color)ApplicationResources.GetResource("DarkGreen");
    private static readonly Color DefaultFocusedBorderColor = (Color)ApplicationResources.GetResource("PrimaryDarkBlue");
    private static readonly Color DefaultUnfocusedBorderColor = (Color)ApplicationResources.GetResource("LightGray");
    private static readonly Color DefaultPlaceholderColor = Colors.Gray;
    private static readonly Color DefaultCleanButtonBackgroundColor = (Color)ApplicationResources.GetResource("DarkGreen");
    
    public CustomDatePicker()
    {
        InitializeComponent();
        Content.BindingContext = this;
    }
    
    #region Bindable Properties
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultTextColor))] private readonly Color? _textColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultFocusedBorderColor))] private readonly Color? _focusedBorderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultUnfocusedBorderColor))] private readonly Color? _unfocusedBorderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultPlaceholderColor))] private readonly Color? _placeholderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultCleanButtonBackgroundColor))] private readonly Color? _cleanButtonBackgroundColor;
    
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.TwoWay), DefaultValue = "DateTime.Now")] private readonly DateTime _date;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _placeholder;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _helpText;
    
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly EInputState _datePickerState;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Style? _datePickerStyle;
    #endregion
    
    #region Events
    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        DatePicker.Focus();
    }
    
    private void DatePicker_OnFocused(object? sender, FocusEventArgs e)
    {
        // no funcionan en android
    }

    private void DatePicker_OnUnfocused(object? sender, FocusEventArgs e)
    {        
        // no funcionan en android
    }
    #endregion
}