namespace PencApp.Helpers;

public static class ServiceHelper
{
    public static T? GetService<T>() => ContainerLocator.Container.GetContainer().GetService<T>();
}