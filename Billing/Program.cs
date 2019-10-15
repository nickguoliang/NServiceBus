using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Billing
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }
        static async Task AsyncMain()
        {
            Console.Title = "Billing";
            var endpointConfiguation = new EndpointConfiguration(endpointName: "Billing");
            var transport = endpointConfiguation.UseTransport<LearningTransport>();
            var persistence = endpointConfiguation.UsePersistence<LearningPersistence>();
            var endpointInstance = await Endpoint.Start(endpointConfiguation).ConfigureAwait(false);
            Console.WriteLine("Please enter to exit");
            Console.ReadLine();
            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
