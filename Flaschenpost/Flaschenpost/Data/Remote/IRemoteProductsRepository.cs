using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flaschenpost
{
    public interface IRemoteProductsRepository
    {
        Task<IList<Item>> GetItemsAsync(SortId sortId);
    }
}
