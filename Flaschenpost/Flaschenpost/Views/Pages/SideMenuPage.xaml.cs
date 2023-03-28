using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Flaschenpost
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideMenuPage : ContentPage
    {
        public SideMenuPage()
        {
            On<iOS>().SetUseSafeArea(true);

            Title = AppInfo.Name;

            InitializeComponent();
            BindingContext = ServiceHelper.Get<SideMenuViewModel>();
        }
    }
}