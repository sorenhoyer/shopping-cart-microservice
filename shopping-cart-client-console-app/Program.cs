using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace shopping_cart_client_console_app
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var eventSubscriber = new EventSubscriber();
            await eventSubscriber.SubscriptionCycleCallback();
            while (true) ;
        }
    }
}
