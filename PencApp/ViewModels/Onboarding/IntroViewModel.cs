using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PencApp.Services.Exceptions;

namespace PencApp.ViewModels.Onboarding;

public partial class IntroViewModel(INavigationService navigationService, IExceptionService exceptionService)
    : BaseViewModel(navigationService, exceptionService)
{
    [ObservableProperty] private string? _title;
    [ObservableProperty] private string? _message;
    [ObservableProperty] private string? _buttonText;
    [ObservableProperty] private IEnumerable<string>? _introCarouselSource;
    [ObservableProperty] private int _carouselPosition;

    public override void Initialize(INavigationParameters parameters)
    {
        IntroCarouselSource = ["intro_1.png", "intro_2.png", "intro_3.png"];
        GetData(CarouselPosition);
        base.Initialize(parameters);
    }

    partial void OnCarouselPositionChanged(int value)
    {
        GetData(value);
    }

    private void GetData(int position)
    {
        switch (position)
        {
            case 1:
                Title = "Access to a wide range of courses";
                Message = "learn new skills and improve your knowledge from the comfort Of your own phone";
                ButtonText = "Next";
                break;
            case 2:
                Title = "Affordable pricing options";
                Message = "Lorem ipsum dolor sit amet consectetur. Eu placerat dolor lectus nulla sapien I";
                ButtonText = "Start now";
                break;
            default:
                Title = "Upgrade your skills for the future";
                Message = "We'll help you to unlock your full potential by providing the tools you need to achieve your goals";
                ButtonText = "Next";
                break;
        }
    }

    [RelayCommand]
    private async Task Next()
    {
        if (CarouselPosition == IntroCarouselSource?.Count() - 1)
            await GoBackAsync();
    }
}