using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoFac.Demo
{
    /// <summary>
    /// Autofac -- 用Autofac增强容器能力，引入面向切面编程（AOP）的能力
    /// 当有以下场景需求时，可引入第三方框架实现
    ///     基于名称的注入
    ///     属性注入
    ///     子容器
    ///     基于动态代理的AOP
    ///         面向切面程序设计
    ///         在不改变原有类的基础上，在方法执行时嵌入一些逻辑，在执行过程切面上任意的插入我们的逻辑
    /// 核心扩展点
    ///     public interface IServiceProviderFactory<TContainerBuilder>
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // 注入Autofac工厂实现
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
