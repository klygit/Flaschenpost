using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace Flaschenpost
{
    public class RemoteProductsRepository : IRemoteProductsRepository
    {
        private readonly IRemoteProductsSource _remoteProductsSource;

        public RemoteProductsRepository(IRemoteProductsSource remoteProductsSource)
        {
            _remoteProductsSource = remoteProductsSource;
        }

        public async Task<IList<Item>> GetItemsAsync(SortId sortId)
        {
            try
            {
                var responseContent = await _remoteProductsSource.GetContentAsJsonAsync();
                var productDtos = JsonSerializer.Deserialize<List<ProductDto>>(responseContent);
                var items = DtoMapper.ToItems(productDtos);

                SortHelper(items, sortId);

                return items;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            return null;
        }

        private static void SortHelper(List<Item> items, SortId sortId)
        {
            items.Sort((item1, item2) => compare(sortId, item1, item2));
            static int compare(SortId sortId, Item item1, Item item2)
            {
                var compareResult = sortId switch
                {
                    SortId.PriceDesc => item2.Price.CompareTo(item1.Price),
                    SortId.PricePerBottleAsc => item1.PricePerBottle.CompareTo(item2.PricePerBottle),
                    SortId.PricePerBottleDesc => item2.PricePerBottle.CompareTo(item1.PricePerBottle),
                    SortId.PricePerUnitAsc => item1.PricePerUnit.CompareTo(item2.PricePerUnit),
                    SortId.PricePerUnitDesc => item2.PricePerUnit.CompareTo(item1.PricePerUnit),
                    SortId.CountBottleAsc => item1.CountBottle.CompareTo(item2.CountBottle),
                    SortId.CountBottleDesc => item2.CountBottle.CompareTo(item1.CountBottle),
                    SortId.PriceAsc or _ => item1.Price.CompareTo(item2.Price),
                };

                if (compareResult != 0)
                    return compareResult;

                // sort by name next if items are equal
                return item1.Name.CompareTo(item2.Name);
            }
        }
    }
}
