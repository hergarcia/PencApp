namespace PencApp.Resources.Styles;

public static class UiConstants
{
    public const int DefaultCornerRadiusValue = 15;
    public const int SharpCornerRadiusValue = 5;
    public const int WideCornerRadiusValue = 30;
    public static CornerRadius DefaultCornerRadius = new(DefaultCornerRadiusValue);
    public static CornerRadius SharpCornerRadius = new(SharpCornerRadiusValue);
    public static CornerRadius RoundedCornerRadius = new(WideCornerRadiusValue);
    public static CornerRadius TopRoundedSharpCornerRadius = new(SharpCornerRadiusValue, SharpCornerRadiusValue, 0, 0);
    public static CornerRadius TopRoundedDefaultCornerRadius = new(DefaultCornerRadiusValue, DefaultCornerRadiusValue, 0, 0);
    public static CornerRadius TopRoundedWideCornerRadius = new(WideCornerRadiusValue, WideCornerRadiusValue, 0, 0);
}