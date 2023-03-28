
namespace Flaschenpost
{
    public class Item
    {
        public string UrlImage { get; set; }

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string PricePerUnitText { get; set; }

        public double Price { get; set; }
        public string PriceDisplay => $"{Price} €".Replace(".", ",");

        public string DescriptionText { get; set; }


        public double PricePerUnit { get; set; }
        public int CountBottle { get; set; }
        public double PricePerBottle => Price / CountBottle;
    }
}
