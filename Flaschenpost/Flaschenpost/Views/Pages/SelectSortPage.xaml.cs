using System.Globalization;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flaschenpost
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectSortPage : ContentPage
    {
        public SelectSortPage()
        {
            Title = AppInfo.Name;

            InitializeComponent();
            BindingContext = ServiceHelper.Get<SelectSortViewModel>();
        }
    }

    public class BoolToColorConverter : IValueConverter
    {
        public Color TrueObject { set; get; }
        public Color FalseObject { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueObject : FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Color)value).Equals(TrueObject);
        }
    }
}