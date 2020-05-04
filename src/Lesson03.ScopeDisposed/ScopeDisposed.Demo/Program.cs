using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ScopeDisposed.Demo
{
    /// <summary>
    /// 作用域和对象释放行为 -- 你知道IDisposable对象释放的时机和坑嘛？
    ///     对于实现了IDisposable接口类型
    ///         DI只负责释放由其创建的对象实例
    ///         DI在容器或子容器释放时，释放由其创建的对象实例
    ///     避免在根容器获取实现了IDisposable接口的瞬时服务
    ///     避免手动创建实现了IDisposable的对象，应该使用容器来管理其生命周期
    ///     
    /// IHostApplicationLifetime
    ///     用来管理整个应用程序的生命周期
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
