using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    public class PlaceOrderHander:IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrder>();
        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"Receive PlaceOrder,SkuName={message.SkuName},OrderCount={message.OrderCount}");
            var orderPlaced = new OrderPlaced()
            {
                OrderId = message.OrderId,
                SkuName=message.SkuName,
                OrderCount=message.OrderCount
            };
            return context.Publish(orderPlaced);
        }
    }
}
