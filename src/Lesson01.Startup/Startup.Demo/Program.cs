using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Startup.Demo
{
    /// <summary>
    /// Startup -- 掌握ASP.NET Core 的启动过程
    /// 启动执行顺序：
    ///     1. ConfigureWebHostDefaults
    ///        配置了必要组件：容器组件、配置的组件 
    ///     2. ConfigureHostConfiguration
    ///        配置启动时必要的配置：所需要监听的端口、URL等，在这个过程中我们可以配置自己的内容注入到配置框架中
    ///     3. ConfigureAppConfiguration
    ///        让我们来嵌入我们自己的配置文件供应用程序读取
    ///     4. ConfigureServices
    ///        ConfigureLogging
    ///        Startup
    ///        Startup.ConfigureServices
    ///        这几个都是用来往容器里注入我们的应用的组件
    ///     5. Startup.Configure
    ///        注入中间件，处理HttpContext整个的请求的
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
                    Console.WriteLine("ConfigureWebHostDefaults");
                    webBuilder.UseStartup<Startup>();
                    // Startup 类，非必须使用，以下方法同Startup类作用一致
                    // 为了代码结构更合理清晰
                    //webBuilder.ConfigureServices(services =>
                    //{
                    //    Console.WriteLine("webBuilder.ConfigureServices");
                    //    services.AddControllers();
                    //});
                    //webBuilder.Configure(app =>
                    //{
                    //    Console.WriteLine("webBuilder.Configure");

                    //    //if (env.IsDevelopment())
                    //    //{
                    //    //    app.UseDeveloperExceptionPage();
                    //    //}
                    //    app.UseHttpsRedirection();
                    //    app.UseRouting();
                    //    app.UseAuthorization();
                    //    app.UseStaticFiles();
                    //    app.UseWebSockets();
                    //    app.UseEndpoints(endpoints =>
                    //    {
                    //        endpoints.MapControllers();
                    //    });
                    //});
                })
                .ConfigureServices(service =>
                {
                    Console.WriteLine("ConfigureServices");
                })
                .ConfigureAppConfiguration(builder =>
                {
                    Console.WriteLine("ConfigureAppConfiguration");
                })
                .ConfigureHostConfiguration(builder =>
                {
                    Console.WriteLine("ConfigureHostConfiguration");
                });
    }
}
