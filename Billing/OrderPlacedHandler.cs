using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Billing
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Receive OrderPlaced,SkuName={message.SkuName},OrderCount={message.OrderCount},Charging Credit Card...");
            var orderBilled = new OrderBilled()
            {
                OrderId = message.OrderId,
                SkuName=message.SkuName,
                OrderCount=message.OrderCount
            };
            return context.Publish(orderBilled);
        }
    }
}
