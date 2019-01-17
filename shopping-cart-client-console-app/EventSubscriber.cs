using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace shopping_cart_client_console_app
{
    class EventSubscriber { 
        private readonly Timer timer;
        private long start = 0, chunkSize = 10;

        public EventSubscriber()
        {
            this.timer = new Timer(10 * 1000);
            this.timer.AutoReset = false;
            this.timer.Elapsed += (_, __) => SubscriptionCycleCallback().Wait();
        }

        public async Task SubscriptionCycleCallback()
        {
            var response = await ReadEvents();
            if(response.StatusCode == HttpStatusCode.OK)
            {
                HandleEvents(await response.Content.ReadAsStringAsync());
                this.timer.Start();
            }
            return;
        }

        private async Task<HttpResponseMessage> ReadEvents()
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.BaseAddress = new Uri($"https://localhost:5001");

                    var response = await httpClient.GetAsync($"/api/ShoppingCart/events?from="+start+"&to=" + chunkSize);

                    return response; //await response.Content.ReadAsStringAsync();
                }
            }
        }

        private void HandleEvents(string content)
        {
            var events = JsonConvert.DeserializeObject<IEnumerable<Event>>(content);

            Console.WriteLine("Events:");
            Console.WriteLine("");
            foreach (var ev in events)
            {
                Console.WriteLine("Id: " + ev.Id + " Name: " + ev.Name + " Type: " + ev.Type);
            }
        }
    }
}
