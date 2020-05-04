using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IOC.Demo
{
    /// <summary>
    /// 依赖注入(默认框架) -- 良好架构的起点
    ///     核心类型
    ///         IServiceCollection：负责服务注册
    ///         ServiceDescriptor：每个服务注册时的信息
    ///         IServiceProvider：具体的容器，也是由ServiceCollection Build出来的
    ///         IServiceScope：表示一个容器的子容器的生命周期
    ///     生命周期
    ///         Singleton：单例，整个跟容器的作用域内是单例模式
    ///         Scoped：作用域，是指在我的容器或子容器的生存周期内是同一个对象（单例），如果容器释放，对象也会跟着被释放
    ///         Transient：瞬时，每次获取时，都是全新的对象
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
