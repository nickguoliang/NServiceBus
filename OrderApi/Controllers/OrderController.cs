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
        private IEndpointInstance endpointInstance;
        /// <summary>
        /// constructor injection
        /// </summary>
        /// <param name="endpointInstance"></param>
        public OrderController(IEndpointInstance endpointInstance)
        {
            this.endpointInstance = endpointInstance;
        }
        /// <summary>
        /// submit sorder
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        [HttpPost("submitOrder")]
        public ActionResult<ApiResult> submitOrder([FromBody] PlaceOrder model)
        {
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