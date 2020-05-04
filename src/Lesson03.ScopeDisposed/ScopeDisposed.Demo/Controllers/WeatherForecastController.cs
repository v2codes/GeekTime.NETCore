using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScopeDisposed.Demo.Services;

namespace ScopeDisposed.Demo.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get([FromServices]IOrderService orderService1,
                       [FromServices]IOrderService orderService2,
                       [FromServices]IHostApplicationLifetime hostApplicationLifetime, // 用来管理整个应用程序的生命周期
                       [FromQuery]bool stop = false)
        {

            #region 验证 Scoped
            //Console.WriteLine("===========1===========");
            //using (var scope = HttpContext.RequestServices.CreateScope())
            //{
            //    var service = scope.ServiceProvider.GetService<IOrderService>();
            //    var service2 = scope.ServiceProvider.GetService<IOrderService>();
            //}
            //Console.WriteLine("===========2===========");
            #endregion

            #region 验证 Singleton
            //if (stop)
            //{
            //    hostApplicationLifetime.StopApplication();
            //}
            #endregion

            Console.WriteLine("接口处理完毕");
            return 1;
        }
    }
}
