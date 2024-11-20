namespace PencApp.Resources.Styles;

public static class UiConstants
{
    public const int DefaultCornerRadiusValue = 15;
    public const int SharpCornerRadiusValue = 5;
    public const int RoundedCornerRadiusValue = 30;
    public static CornerRadius DefaultCornerRadius = new(DefaultCornerRadiusValue);
    public static CornerRadius SharpCornerRadius = new(SharpCornerRadiusValue);
    public static CornerRadius RoundedCornerRadius = new(RoundedCornerRadiusValue);
    public static CornerRadius TopRoundedSharpCornerRadius = new(SharpCornerRadiusValue, SharpCornerRadiusValue, 0, 0);
    public static CornerRadius TopRoundedDefaultCornerRadius = new(DefaultCornerRadiusValue, DefaultCornerRadiusValue, 0, 0);
}