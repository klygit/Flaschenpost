using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flaschenpost
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            Title = AppInfo.Name;
            InitializeComponent();
            BindingContext = ServiceHelper.Get<AboutViewModel>();
        }
    }
}