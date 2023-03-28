using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flaschenpost
{
    internal class DataManager : IDataManager
    {
        private IRemoteProductsRepository _remoteProductsRepository;

        public DataManager(IRemoteProductsRepository remoteProductsRepository)
        {
            _remoteProductsRepository = remoteProductsRepository;
        }

        public async Task<IList<Item>> GetItemAsync(SortId sortId)
        {
            return await _remoteProductsRepository.GetItemsAsync(sortId);
        }
    }
}
