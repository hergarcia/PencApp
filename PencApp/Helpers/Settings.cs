namespace PencApp.Helpers;

public static class Settings
{
    public static bool UseMock
    {
        get => Preferences.Default.Get(nameof(UseMock), false);
        set => Preferences.Default.Set(nameof(UseMock), value);
    }
}