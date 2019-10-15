using Messages;
using NServiceBus;
using NServiceBus.Features;
using System;
using System.Threading.Tasks;

namespace Shipping
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }
        static async Task AsyncMain()
        {
            Console.Title = "Shipping";
            var endpointConfiguation = new EndpointConfiguration(endpointName: "Shipping");
            var transport = endpointConfiguation.UseTransport<LearningTransport>();
            //endpointConfiguation.DisableFeature<Sagas>();
            var persistence = endpointConfiguation.UsePersistence<LearningPersistence>();
            
            var endpointInstance = await Endpoint.Start(endpointConfiguation).ConfigureAwait(false);
            Console.WriteLine("Please enter to exit");
            Console.ReadLine();
            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
