using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;
using PencApp.Helpers;

namespace PencApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .UsePrism(PrismStartup.Configure)
            .UseMauiCommunityToolkit()
            .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("NotoSans-Light.ttf", "NSLight");
                    fonts.AddFont("NotoSans-Regular.ttf", "NSRegular");
                    fonts.AddFont("NotoSans-Medium.ttf", "NSMedium");
                    fonts.AddFont("NotoSans-Bold.ttf", "NSBold");
                    fonts.AddFont("NotoSans-ExtraBold.ttf", "NSExtraBold");
                });
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
        InitCustomHandlers();
        return builder.Build();
    }
    
    private static void InitCustomHandlers()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
        {
#if IOS
            h.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#else
            h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif
        });

        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("customDP", (h, v) =>
        {
#if IOS
            h.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#else
            h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif
        });

        Microsoft.Maui.Handlers.TimePickerHandler.Mapper.AppendToMapping("customTP", (h, v) =>
        {
#if IOS
            h.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#else
            h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif
        });

        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("customPH", (h, v) =>
        {
#if IOS
            h.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#else
            h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif
        });
    }
}