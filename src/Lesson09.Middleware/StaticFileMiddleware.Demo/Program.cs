using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StaticFileMiddleware.Demo
{
    /// <summary>
    /// 静态文件中间件 -- 前后端分离开发、合并部署骚操作
    ///     app.UseStaticFiles();
    ///     能力
    ///         支持指定相对路径
    ///         支持目录浏览:services.AddDirectoryBrowser(); app.UseDirectoryBrowser();
    ///         支持设置默认文档:app.UseDefaultFiles();
    ///         支持多目录映射
    /// 
    /// 实现前端HTML5 History 路由模式支持
    ///     配置除了api之外的请求，都响应index.html
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
