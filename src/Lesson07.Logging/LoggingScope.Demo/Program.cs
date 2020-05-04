using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LoggingScope.Demo
{
    /// <summary>
    /// 日志作用域 -- 解决不同请求之间的日志干扰
    ///     启用作用域
    ///         "IncludeScopes": true,
    ///     场景
    ///         一个事务包含多条操作时
    ///         复杂流程的日志关联时
    ///         调用链追踪与请求处理过程对应时
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
                builder.AddDebug();
            });

            IServiceProvider service = serviceClollection.BuildServiceProvider();

            var logger = service.GetService<ILogger<Program>>();

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                using (logger.BeginScope("ScopeId:{scopeId}", Guid.NewGuid()))
                {
                    logger.LogInformation("这是Info");
                    logger.LogError("这是Error");
                    logger.LogTrace("这是Trace");
                }
                System.Threading.Thread.Sleep(100);
                Console.WriteLine("===========================================");
            }

            Console.ReadLine();
        }
    }
}
