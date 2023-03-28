using System.Threading.Tasks;

namespace Flaschenpost
{
    public interface IRemoteProductsSource
    {
        Task<string> GetContentAsJsonAsync();
    }
}
