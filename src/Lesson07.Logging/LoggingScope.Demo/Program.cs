using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LoggingScope.Demo
{
    /// <summary>
    /// 日志作用域 -- 解决不同请求之间的日志干扰
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args);
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

            // 获取 Logger 对象
            var logger = service.GetService<ILogger<Program>>();
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                // 启用作用域
                using (logger.BeginScope("ScopeId:{scopeId}", Guid.NewGuid()))
                {
                    logger.LogInformation("这是Info");
                    logger.LogError("这是Error");
                    logger.LogTrace("这是Trace");
                }

                // 如果不加Sleep，输出顺序会错乱，因为 Console日志 的内部是异步实现
                System.Threading.Thread.Sleep(100);
                Console.WriteLine("===========================================");
            }

            Console.ReadLine();
        }
    }
}
