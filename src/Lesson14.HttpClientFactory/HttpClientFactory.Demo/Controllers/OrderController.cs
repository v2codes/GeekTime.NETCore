using HttpClientFactory.Demo.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientFactory.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderClient _orderClient;
        private readonly NamedOrderClient _namedOrderClient;
        private readonly TypedOrderClient _typedOrderClient;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger,
                               OrderClient orderClient,
                               NamedOrderClient namedOrderClient,
                               TypedOrderClient typedOrderClient)
        {
            _logger = logger;
            _orderClient = orderClient;
            _namedOrderClient = namedOrderClient;
            _typedOrderClient = typedOrderClient;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var result = await _orderClient.Get();
            return result;
        }

        [HttpGet]
        [Route("/named")]
        public async Task<string> NamedGet()
        {
            var result = await _namedOrderClient.Get();
            return result;
        }

        [HttpGet]
        [Route("/typed")]
        public async Task<string> TypedGet()
        {
            var result = await _typedOrderClient.Get();
            return result;
        }
    }
}
