using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flaschenpost
{
    internal interface IDataManager
    {
        Task<IList<Item>> GetItemAsync(SortId sortId);
    }
}
