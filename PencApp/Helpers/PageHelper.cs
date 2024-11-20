using PencApp.Extensions;

namespace PencApp.Helpers;

public static class PageHelper
{
    public static Page GetCurrentPage()
    {
        var currentPage = Application.Current.MainPage;
        if (currentPage is FlyoutPage flyoutPage)
        {
            var flyoutPageDetail = flyoutPage.Detail;
            var navigation = flyoutPageDetail.Navigation;

            var navigationModalStack = navigation.ModalStack;
            if (navigationModalStack.Count() > 0)
                return navigationModalStack.Last();

            var navigationStack = navigation.NavigationStack;
            return navigationStack.Last();
        }

        return currentPage;
    }

    public static bool IsCurrentPageModal() => GetCurrentPage().IsModal();
}