using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace shopping_cart_client_console_app
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var t = ReadEvents();
                t.Wait();
                Console.WriteLine(t.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task<string> ReadEvents()
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.BaseAddress = new Uri($"https://localhost:5001");

                    var response = await httpClient.GetAsync($"/api/ShoppingCart/events?from=0&to=3");

                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
