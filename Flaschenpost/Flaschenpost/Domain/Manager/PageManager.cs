using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace Flaschenpost
{
    internal class PageManager : IPageManager
    {
        private static readonly Color _navPageBarBackgroundColor = Color.FromHex("#333333");
        private static readonly Color _navPageBarTextColor = Color.FromHex("#ffffff");

        private FlyoutPage _flyoutPage;
        private Page _sideMenuPage;
        private NavigationPage _navigationPage;

        public void Init(App app, Page root)
        {
            _sideMenuPage = new SideMenuPage();
            _navigationPage = new NavigationPage(root)
            {
                BarBackgroundColor = _navPageBarBackgroundColor,
                BarTextColor = _navPageBarTextColor,
            };

            _flyoutPage = new FlyoutPage()
            {
                Flyout = _sideMenuPage,
                Detail = _navigationPage,
            };

            app.MainPage = _flyoutPage;
        }

        public async Task PushPageAsync(Page page, bool modal = false)
        {
            _flyoutPage.IsPresented = false;
            if (modal)
            {
                // wrap page around navigationPage for titlebar
                var modalPage = new NavigationPage(page)
                {
                    BarBackgroundColor = _navPageBarBackgroundColor,
                    BarTextColor = _navPageBarTextColor,
                };
                modalPage.ToolbarItems.Add(new ToolbarItem("Close", "ic_tb__cancel.png", async () => await PopPageAsync(true)));
                await _navigationPage?.Navigation?.PushModalAsync(modalPage);
                return;
            }
            await _navigationPage?.Navigation?.PushAsync(page);
        }

        public async Task PopPageAsync(bool modal = false)
        {
            _flyoutPage.IsPresented = false;
            if (modal)
            {
                await _navigationPage?.Navigation?.PopModalAsync();
                return;
            }
            await _navigationPage?.Navigation?.PopAsync();
        }

        public async Task DisplayToastAsync(string message, int durationMilliseconds = 3000)
        {
            await _navigationPage?.DisplayToastAsync(message, durationMilliseconds);
        }
    }
}
