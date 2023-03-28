using System.Threading.Tasks;
using Xamarin.Forms;

namespace Flaschenpost
{
    public interface IPageManager
    {
        void Init(App app, Page root);
        Task PushPageAsync(Page page, bool modal = false);
        Task PopPageAsync(bool modal = false);
        Task DisplayToastAsync(string message, int durationMilliseconds = 3000);
    }
}