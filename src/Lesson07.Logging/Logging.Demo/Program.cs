using Logging.Demo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Logging.Demo
{
    /// <summary>
    /// 日志框架 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 从文件中读取配置
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = builder.Build();

            // 构造服务容器
            IServiceCollection serviceClollection = new ServiceCollection();
            // 用工厂模式将配置对象注册到容器管理器，这样做，容器就可以帮我们管理这个对象的生命周期
            serviceClollection.AddSingleton<IConfiguration>(p => config);

            // 注入 Logging 服务、添加日志配置、 添加Consul日志输出
            serviceClollection.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
            });


            serviceClollection.AddTransient<OrderService>();

            IServiceProvider service = serviceClollection.BuildServiceProvider();

            var orderService = service.GetService<OrderService>();
            orderService.Show();

            // ILoggerFactory 方式获取日志对象
            //ILoggerFactory loggerFactory = service.GetService<ILoggerFactory>();
            //ILogger alogger = loggerFactory.CreateLogger("alogger");
            //alogger.LogDebug("aiya logDebug.");
            //alogger.LogInformation("hello logInformartion.");
            //var ex = new Exception("出错了");
            //alogger.LogError(ex, "出错了");

            // ILogger<T> 强类型泛型模式获取 
            //var logger = service.GetService<ILogger<Program>>();
            //logger.LogInformation(new EventId(201, "xihuaa"), "Hello world!");


            Console.ReadLine();
        }
    }
}
