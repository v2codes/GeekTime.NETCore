using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Middleware.Demo.Middlewares;

namespace Middleware.Demo
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Use 指定全局中间件
            //app.Use(async (context, next) =>
            //{
            //    // 不执行 next时，收到的所有响应结果都会是 Hello，因为当前中间件注册的最早，而且断路了后续中间件。
            //    ////await context.Response.WriteAsync("Hello");
            //    //await next();

            //    // 执行了 next，再执行Response.Write将会出错，因为一旦 Response 开始输出，就不能对 header 进行操作了。
            //    if (context.Response.HasStarted)
            //    {
            //        //一旦已经开始输出，则不能再修改响应头的内容
            //    }
            //    await context.Response.WriteAsync("Hello2");
            //});
            #endregion

            #region Map 对特定路由地址指定中间
            //app.Map("/abc", abcBuilder =>
            //{
            //    abcBuilder.Use(async (context, next) =>
            //    {
            //        //await context.Response.WriteAsync("Hello");
            //        await next();
            //        await context.Response.WriteAsync("Hello2");
            //    });
            //});

            //app.MapWhen(context => {
            //    return context.Request.Query.Keys.Contains("abc");
            //}, builder =>
            //{
            //    // 与builder.Use方法区别
            //    // use 是指我们可以像注册一个完整的中间件一样，将我们的next也注入进来，在中间件中可以决定是否执行后续的中间件
            //    // Run 则表示当前逻辑为中间件执行的末端，不再继续执行后面的中间件，直接返回了
            //    builder.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("new abc");
            //    });
            //});
            #endregion

            #region 自定义中间件
            //app.UseMyMiddleware();
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
