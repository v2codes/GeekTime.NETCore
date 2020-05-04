using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Routing.Demo
{
    /// <summary>
    /// 路由与终结点 -- 如何规划好你的Web API
    ///     注册方式
    ///         路由模板方式
    ///         RouteAttribute方式
    ///     路由约束
    ///         类型约束
    ///         范围约束
    ///         正则表达式
    ///         是否必选
    ///         自定义IRouteConstraint
    ///     URL生成
    ///         用来反向的根据路由信息生成URL地址
    ///         LinkGenerator
    ///         IUrlHelper
    ///         
    /// Web API 
    ///     Restful 不是必须的
    ///     约定好 API 的表达契约
    ///     将API约束在特定目录下，如/api/
    ///     使用 ObsoleteAttribute 标记为即将废弃的API
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
