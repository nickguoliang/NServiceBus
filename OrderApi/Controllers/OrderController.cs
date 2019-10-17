using Messages;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Common;
using NServiceBus;
using System.Threading.Tasks;

namespace OrderApi.Controllers
{
    /// <summary>
    /// Order Service
    /// </summary>
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : Controller
    {
        /// <summary>
        /// submit sorder
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        [HttpPost("submitOrder")]
        public ActionResult<ApiResult> submitOrder([FromBody] PlaceOrder model)
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName: "OrderAPI");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Order");
            IEndpointInstance endpointInstance = Endpoint.Start(endpointConfiguration).Result;
            var command = new PlaceOrder()
            {
                OrderId = System.Guid.NewGuid().ToString(),
                SkuName=model.SkuName,
                OrderCount=model.OrderCount
            };
            endpointInstance.Send(command).ConfigureAwait(false);
            return new ApiResult() { code = "0", errorMsg = "" };
        }
    }
}