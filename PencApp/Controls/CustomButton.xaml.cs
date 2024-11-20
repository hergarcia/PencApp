using System.Windows.Input;
using Maui.BindableProperty.Generator.Core;

namespace PencApp.Controls;

public partial class CustomButton : ContentView
{
    public CustomButton()
    {
        InitializeComponent();
        Content.BindingContext = this;
    }

    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _text;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _leftImageSource;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _rightImageSource;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _textColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _leftImageColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _rightImageColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Color? _backgroundColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly ICommand? _command;
}