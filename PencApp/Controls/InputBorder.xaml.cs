using Maui.BindableProperty.Generator.Core;
using Microsoft.Maui.Controls.Shapes;
using PencApp.Helpers;
using PencApp.Resources.Styles;

namespace PencApp.Controls;

public partial class InputBorder : Border
{
    private static readonly Color DefaultFocusedBorderColor = (Color)ApplicationResources.GetResource("BlueHeader");
    private static readonly Color DefaultUnfocusedBorderColor = (Color)ApplicationResources.GetResource("LightGray");

    public InputBorder()
    {
        // Loaded += OnLoaded;
        InitializeComponent();
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
        var index = -1;
        var itemCount = 0;
        var elementParent = Parent;

        while (elementParent is not IInputBase)
        {
            if (elementParent.Parent is not null)
            {
                elementParent = elementParent.Parent;
            }
            else
            {
                break;
            }
        }

        if (elementParent is not IInputBase)
        {
            return;
        }
        
        var inputElement = elementParent as View;
        
        switch (inputElement!.Parent)
        {
            case StackBase sb:
                index = sb.Children.IndexOf(inputElement);
                itemCount = sb.Children.Count;
                break;
            case StructuredItemsView si:
            {
                var items = si.ItemsSource.Cast<object>().ToList();
                index = items.IndexOf(inputElement);
                itemCount = items.Count;
                break;
            }
        }

        if (itemCount > 1 && index == 0)
        {
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new CornerRadius(
                    Convert.ToDouble(UiConstants.DefaultCornerRadiusValue),
                    Convert.ToDouble(UiConstants.DefaultCornerRadiusValue),
                    Convert.ToDouble(UiConstants.SharpCornerRadiusValue),
                    Convert.ToDouble(UiConstants.SharpCornerRadiusValue))
            };
        }
        else if (itemCount > 1 && index == itemCount - 1)
        {
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new CornerRadius(
                    Convert.ToDouble(UiConstants.SharpCornerRadiusValue),
                    Convert.ToDouble(UiConstants.SharpCornerRadiusValue),
                    Convert.ToDouble(UiConstants.DefaultCornerRadiusValue),
                    Convert.ToDouble(UiConstants.DefaultCornerRadiusValue))
            };
        }
    }

    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultFocusedBorderColor))] private readonly Color? _focusedBorderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultUnfocusedBorderColor))] private readonly Color? _unfocusedBorderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly EInputState _inputState;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly bool? _isInputFocused;
}