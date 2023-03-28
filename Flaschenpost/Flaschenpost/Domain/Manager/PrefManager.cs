using Xamarin.Essentials;

namespace Flaschenpost
{
    internal class PrefManager : IPrefManager
    {
        private string Key__SortId => "Key__SortId";

        public void SetSortId(SortId sortId)
        {
            Preferences.Set(Key__SortId, (int)sortId);
        }

        public SortId GetSortId()
        {
            var id = Preferences.Get(Key__SortId, (int)SortId.PriceAsc);
            return (SortId)id;
        }

    }
}
