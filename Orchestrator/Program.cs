using Messages;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Orchestrator
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }
        static async Task AsyncMain()
        {
            Console.Title = "Orchestrator";
            var endpointConfiguation = new EndpointConfiguration(endpointName: "Orchestrator");
            var transport = endpointConfiguation.UseTransport<LearningTransport>();
            var persistence = endpointConfiguation.UsePersistence<LearningPersistence>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(ShipOrder), "Shipping");
            var endpointInstance = await Endpoint.Start(endpointConfiguation).ConfigureAwait(false);
            Console.WriteLine("Please enter to exit");
            Console.ReadLine();
            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
