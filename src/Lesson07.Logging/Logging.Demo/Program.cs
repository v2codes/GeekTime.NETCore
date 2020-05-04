using Logging.Demo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Logging.Demo
{
    /// <summary>
    /// 日志框架 -- 聊聊记日志的最佳姿势
    ///     引用包
    ///         Microsoft.Extensions.Logging
    ///         Microsoft.Extensions.Logging.Console
    ///         Microsoft.Extensions.Logging.Debug
    ///         Microsoft.Extensions.Logging.TraceSource
    ///         
    ///     日志级别
    ///         Trace、Critical、Error、Warning、Information、Debug、Trace 6-0
    ///         
    ///     日志对象获取
    ///         ILoggerFactory 方式获取
    ///         ILogger<T> 强类型泛型模式获取
    ///         
    ///     日志过滤的配置逻辑    
    ///         根据日志名称，配置日志级别、开关等
    ///         
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = builder.Build();

            IServiceCollection serviceClollection = new ServiceCollection();
            serviceClollection.AddSingleton<IConfiguration>(p => config); // 用工厂模式将配置对象注册到容器管理器

            serviceClollection.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
            });

            serviceClollection.AddTransient<OrderService>();

            IServiceProvider service = serviceClollection.BuildServiceProvider();

            //ILoggerFactory 方式获取日志对象
            //ILoggerFactory loggerFactory = service.GetService<ILoggerFactory>();
            //ILogger alogger = loggerFactory.CreateLogger("alogger");
            //alogger.LogDebug("aiya logDebug.");
            //alogger.LogInformation("hello logInformartion.");
            //var ex = new Exception("出错了");
            //alogger.LogError(ex, "出错了");

            var orderService = service.GetService<OrderService>();
            orderService.Show();


            //var logger = service.GetService<ILogger<Program>>();
            //logger.LogInformation(new EventId(201, "xihuaa"), "Hello world!");

            Console.ReadLine();
        }
    }
}
