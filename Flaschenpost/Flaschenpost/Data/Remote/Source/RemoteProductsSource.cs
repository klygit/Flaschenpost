using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Flaschenpost
{
    internal class RemoteProductsSource : IRemoteProductsSource
    {
        public async Task<string> GetContentAsJsonAsync()
        {
            var apiUrl = "https://flapotest.blob.core.windows.net/test/ProductData.json";

            var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return null;
        }
    }
}
