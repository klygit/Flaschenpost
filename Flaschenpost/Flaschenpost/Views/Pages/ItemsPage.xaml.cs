using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flaschenpost
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        private readonly ItemsViewModel _viewModel;

        public ItemsPage()
        {
            if (Device.RuntimePlatform != Device.iOS)
                Title = AppInfo.Name;
            InitializeComponent();
            BindingContext = _viewModel = ServiceHelper.Get<ItemsViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearingAsync();
        }
    }
}