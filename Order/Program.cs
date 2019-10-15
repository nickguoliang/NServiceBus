using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }
        static async Task AsyncMain()
        {
            Console.Title = "Order";
            var endpointConfiguration = new EndpointConfiguration("Order");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            Console.WriteLine("Please enter to exit");
            Console.ReadLine();
            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
