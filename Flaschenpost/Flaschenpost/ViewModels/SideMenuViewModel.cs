using System.Windows.Input;
using Xamarin.Forms;

namespace Flaschenpost
{
    internal class SideMenuViewModel : BaseViewModel
    {
        public ImageSource ImgSrcSideMenuTitleLogo => ImgResHelper.GetImgSrc("flaschenpost_logo.png");

        public string SideMenuItemAboutTitle => "About";

        public ICommand ShowAboutPageCommand { get; private set; }

        public SideMenuViewModel(IPageManager pageManager)
        {
            ShowAboutPageCommand = new Command(async () => await pageManager.PushPageAsync(new AboutPage(), true));
        }
    }
}
