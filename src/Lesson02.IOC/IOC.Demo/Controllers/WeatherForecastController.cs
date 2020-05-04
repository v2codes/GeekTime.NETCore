using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOC.Demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IOC.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGerericService<IOrderService, int> _gerericService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGerericService<IOrderService, int> gerericService)
        {
            _gerericService = gerericService;
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        public int GetServices([FromServices]IMySingletonService singletonService1,
                               [FromServices]IMySingletonService singletonService2,
                               [FromServices]IMyTransientService transientService1,
                               [FromServices]IMyTransientService transientService2,
                               [FromServices]IMyScopedService scopedService1,
                               [FromServices]IMyScopedService scopedService2)
        {
            Console.WriteLine($" singletonService1 的 HashCode={singletonService1.GetHashCode()}");
            Console.WriteLine($" singletonService2 的 HashCode={singletonService2.GetHashCode()}");

            Console.WriteLine($" transientService1 的 HashCode={transientService1.GetHashCode()}");
            Console.WriteLine($" transientService2 的 HashCode={transientService2.GetHashCode()}");

            Console.WriteLine($" scopedService1 的 HashCode={scopedService1.GetHashCode()}");
            Console.WriteLine($" scopedService2 的 HashCode={scopedService2.GetHashCode()}");
            return 1;
        }

        [HttpGet("/{controller}/getallservices")]
        public int GetAllServices([FromServices] IEnumerable<IOrderService> services)
        {
            foreach (var item in services)
            {
                Console.WriteLine($"获取到IOrderService服务实例：{item.ToString()}  HashCode：{item.GetHashCode()}");
            }
            return 1;
        }
    }
}
