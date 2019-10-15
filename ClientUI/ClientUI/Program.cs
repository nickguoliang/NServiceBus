using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace ClientUI
{
    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }
        static async Task AsyncMain()
        {
            Console.Title = "ClientUI";
            var endpointConfiguration = new EndpointConfiguration(endpointName: "ClientUI");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder),"Order");
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            //Console.WriteLine("Press Entier to exit");
            //Console.ReadLine();
            await RunLoop(endpointInstance).ConfigureAwait(false);
            await endpointInstance.Stop().ConfigureAwait(false);
              
        }
        static async Task RunLoop(IEndpointInstance instance)
        {
            while (true)
            {
                log.Info("Press 'P' to PlaceOrder or 'Q' to Quit");
                var key = Console.ReadKey();
                Console.WriteLine();
                switch (key.Key)
                {
                    case ConsoleKey.P:
                        var command = new PlaceOrder()
                        {
                            OrderId = System.Guid.NewGuid().ToString()
                        };
                        await instance.Send(command).ConfigureAwait(false);

                        break;
                    case ConsoleKey.Q:
                        return;
                    default:
                        log.Info("Unknow input,Please try again");
                        break;
                }
            }
        }
    }
}
