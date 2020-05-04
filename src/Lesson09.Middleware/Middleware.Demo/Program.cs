using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Middleware.Demo
{
    /// <summary>
    /// 中间件：掌控请求处理过程的关键
    ///     工作原理：（俄罗斯套娃）
    ///                     Middleware1         Middleware2         Middleware3     
    ///         Request →    //logic
    ///                       next();     →      //logic
    ///                                           next();     →      //logic
    ///                                                       
    ///                                                               //more logic
    ///                                           //more logic   ← 
    ///                      //more logic   ←
    ///         Response ←
    ///         
    ///     核心对象
    ///         IApplicationBuilder
    ///             注册中间件
    ///         RequestDelegate
    ///             处理整个请求的委托
    ///             
    ///     自定义中间件（约定的方式）
    ///         包含一个 Invoke/InvokeAsync方法，参数为 HttpContext
    ///         
    ///         
    ///        
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
