using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flaschenpost
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(Item item)
        {
            Title = AppInfo.Name;
            InitializeComponent();

            var viewModel = ServiceHelper.Get<ItemDetailViewModel>();
            viewModel.Item = item;

            BindingContext = viewModel;
        }
    }
}