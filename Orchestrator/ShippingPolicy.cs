using Messages;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Unicast.Subscriptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    public class ShippingPolicy: Saga<ShippingPolicyData>, IAmStartedByMessages<OrderPlaced>, IAmStartedByMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<ShippingPolicy>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShippingPolicyData> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(message => message.OrderId)
                            .ToSaga(sagaData => sagaData.OrderId);
            mapper.ConfigureMapping<OrderBilled>(message => message.OrderId)
                            .ToSaga(sagaData => sagaData.OrderId);
        }
        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Received orderPlaced,SkuName={message.SkuName},OrderCount={message.OrderCount}");
            Data.IsOrderPlaced = true;
            return ProcessOrder(context);
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"Received OrderBilled,OrderId={message.OrderId}");
            Data.IsOrderBilled = true;
            return ProcessOrder(context);
        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.IsOrderPlaced && Data.IsOrderBilled)
            {
                await context.Send(new ShipOrder() { OrderId=Data.OrderId});
                MarkAsComplete();
            }
        }
    }
}
