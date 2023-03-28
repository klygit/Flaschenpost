
namespace Flaschenpost
{
    internal class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public string DescriptionDisplay => Item?.DescriptionText;
    }
}
