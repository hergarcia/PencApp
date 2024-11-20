using CommunityToolkit.Maui;

namespace PencApp.Helpers;

public static class ApplicationResources
{
    public static object? GetResource(string resourceKey)
    {
        if (!Application.Current.Resources.TryGetValue(resourceKey, out object? resourceValue))
            throw new Exception($"Resource key {resourceKey} not found");
        
        if (resourceValue is AppThemeColor resource)
        {
            return Application.Current.RequestedTheme == AppTheme.Light ? resource.Light : resource.Dark;
        }

        return resourceValue;
    }
}