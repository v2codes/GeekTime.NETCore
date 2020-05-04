using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StaticFileMiddleware.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 注册目录浏览服务
            //services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // 设置默认访问 index 文件
            //app.UseDefaultFiles();

            // 启用目录浏览
            //app.UseDirectoryBrowser();

            // 启动静态文件中间件
            app.UseStaticFiles();

            // 设置自定义目录静态文件
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    RequestPath="/files", // 设置路由映射
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "assets"))
            //});

            // 配置除了api之外的请求，都响应index.html
            // /order/get
            app.MapWhen(context =>
            {
                return !context.Request.Path.Value.StartsWith("/api");
            }, appBuilder =>
            {
                // Rewrite 方式重定向请求到 index.html
                //var option = new RewriteOptions();
                //option.AddRewrite(".*", "/index.html", true);
                //appBuilder.UseRewriter(option);
                //appBuilder.UseStaticFiles();

                // Run 熔断器方式
                // 缺点：没办法像静态文件中间件那样输出正确的Http请求头
                appBuilder.Run(async context =>
                {
                    const int BufferSize = 64 * 1024;
                    var file = env.WebRootFileProvider.GetFileInfo("index.html");
                    context.Response.ContentType = "text/html";
                    using (var fileStream = new FileStream(file.PhysicalPath,FileMode.Open,FileAccess.Read))
                    {
                        await StreamCopyOperation.CopyToAsync(fileStream, context.Response.Body, null, BufferSize, context.RequestAborted);
                    }
                });



            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
