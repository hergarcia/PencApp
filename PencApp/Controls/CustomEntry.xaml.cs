using System.ComponentModel;
using System.Windows.Input;
using Maui.BindableProperty.Generator.Core;
using PencApp.Helpers;
using PencApp.Resources.Languages;

namespace PencApp.Controls;

public partial class CustomEntry : ContentView, IInputBase
{
    private static readonly Color DefaultTextColor = (Color)ApplicationResources.GetResource("DarkGreen");
    private static readonly Color DefaultFocusedBorderColor = (Color)ApplicationResources.GetResource("PrimaryDarkBlue");
    private static readonly Color DefaultUnfocusedBorderColor = (Color)ApplicationResources.GetResource("LightGray");
    private static readonly Color DefaultPlaceholderColor = Colors.Gray;
    private static readonly Color DefaultCleanButtonBackgroundColor = (Color)ApplicationResources.GetResource("DarkGreen");
    private bool _isPlaceholderAnimated;
    
    public CustomEntry()
    {
        InitializeComponent();
        Content.BindingContext = this;
        CleanButton.IsVisible = false;
        CleanButton.IsEnabled = false;
    }

    #region Bindable Properties
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultTextColor))] private readonly Color? _textColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultFocusedBorderColor))] private readonly Color? _focusedBorderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultUnfocusedBorderColor))] private readonly Color? _unfocusedBorderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultPlaceholderColor))] private readonly Color? _placeholderColor;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay), DefaultValue = nameof(DefaultCleanButtonBackgroundColor))] private readonly Color? _cleanButtonBackgroundColor;
    
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.TwoWay))] private readonly string? _text;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _placeholder;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly string? _helpText;
    
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly bool _hasCleanButton;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly bool _isPassword;
    
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly EInputState _entryState;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly ReturnType _returnType;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Keyboard? _keyboard;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly ICommand? _textChangedCommand;
    [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWay))] private readonly Style? _entryStyle;
    #endregion

    #region Partial methods
    partial void OnTextChanged(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            FocusAnimatePlaceHolder();
        }

        SetCleanButtonVisibility();
        SetShowPasswordVisibility();
    }

    partial void OnIsPasswordChanged(bool value)
    {
        Entry.IsPassword = value;
    }

    partial void OnTextChangedCommandChanged(ICommand? value)
    {
        if (value is not null)
        {
            Entry.TextChanged += (sender, args) => value.Execute(args);
        }
    }

    #endregion
    
    #region Events
    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        if (EntryState == EInputState.Disabled) return;
        Entry.Focus();
    }
    
    private void Entry_OnFocused(object? sender, FocusEventArgs e)
    {
        SetCleanButtonVisibility();
        if (!string.IsNullOrEmpty(Placeholder) && string.IsNullOrEmpty(Text))
        {
            FocusAnimatePlaceHolder();
        }
    }

    private void Entry_OnUnfocused(object? sender, FocusEventArgs e)
    {
        SetCleanButtonVisibility();
        if (!string.IsNullOrEmpty(Placeholder) && string.IsNullOrEmpty(Text))
        {
            UnfocusAnimatePlaceHolder();
        }
    }
    
    private void CleanButton_OnClicked(object? sender, EventArgs e)
    {
        if (EntryState == EInputState.Disabled) return;
        Text = string.Empty;
    }
    #endregion
    
    #region Private methods
    private void FocusAnimatePlaceHolder()
    {
        if (_isPlaceholderAnimated) return;

        Task.Run(() =>
        {
            double targetTranslationY = -1;
            while (targetTranslationY < 0)
            {        
                targetTranslationY = PlaceholderLabel.Y - PlaceholderLabel.Margin.Top;
            }
        
            Animation focusPlaceHolderAnimation = new()
            {
                { 0, 1, new Animation(x => PlaceholderLabel.FontSize = x, PlaceholderLabel.FontSize, 10) },
                { 0, 1, new Animation(x => PlaceholderLabel.TranslationY = x, PlaceholderLabel.TranslationY, -targetTranslationY) },
            };

            _isPlaceholderAnimated = true;
            MainThread.BeginInvokeOnMainThread(()=> focusPlaceHolderAnimation.Commit(this, "FocusAnimatePlaceHolder", easing: Easing.CubicOut));
        });
    }

    private void UnfocusAnimatePlaceHolder()
    {
        if (!_isPlaceholderAnimated) return;
        
        Task.Run(() =>
        {
            Animation unfocusPlaceHolderAnimation = new()
            {
                { 0, 1, new Animation(x => PlaceholderLabel.FontSize = x, PlaceholderLabel.FontSize, 12) },
                { 0, 1, new Animation(x => PlaceholderLabel.TranslationY = x, PlaceholderLabel.TranslationY, 0) },
            };
            
            _isPlaceholderAnimated = false;
            MainThread.BeginInvokeOnMainThread(() => unfocusPlaceHolderAnimation.Commit(this, "UnfocusAnimatePlaceHolder", easing: Easing.CubicOut));
        });
    }

    private void SetCleanButtonVisibility()
    {
        if (EntryState == EInputState.Disabled) return;
        if (!HasCleanButton) return;
        
        CleanButton.IsVisible = !string.IsNullOrEmpty(Text) && Entry.IsFocused;
        CleanButton.IsEnabled = !string.IsNullOrEmpty(Text) && Entry.IsFocused;
    }

    private void SetShowPasswordVisibility()
    {
        if (EntryState == EInputState.Disabled) return;
        if (!IsPassword) return;
        
        ShowPasswordButton.IsVisible = !string.IsNullOrEmpty(Text) && IsPassword;
        ShowPasswordButton.IsEnabled = !string.IsNullOrEmpty(Text) && IsPassword;
    }
    
    private void ShowPasswordButton_OnClicked(object? sender, EventArgs e)
    {
        if (EntryState == EInputState.Disabled) return;
        Entry.IsPassword = !Entry.IsPassword;
    }

    private void Entry_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Entry.IsPassword))
        {
            ShowPasswordButton.Text = Entry.IsPassword ? AppResources.Show : AppResources.Hide;
        }
        
    }    
    #endregion
}