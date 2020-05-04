using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScopeDisposed.Demo.Services;

namespace ScopeDisposed.Demo
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

            //瞬时：接口请求完毕，释放对象 
            services.AddTransient<IOrderService, DisposableOrderService>();

            //作用域：作用域内只能获得同一对象
            //services.AddScoped<IOrderService, DisposableOrderService>();

            //单例：根容器以及子容器内只能获得同一对象
            // 容器自身创建的对象，应用程序退出时会释放所有实现了 IDisposable 的对象
            //services.AddSingleton<IOrderService>(services => new DisposableOrderService());
            //services.AddScoped<IOrderService>(services => new DisposableOrderService());
            //services.AddTransient<IOrderService>(services => new DisposableOrderService());

            // 我们手动自己创建的对象，应用程序退出时不会被自动释放
            //var orderService = new DisposableOrderService();
            //services.AddSingleton<IOrderService>(orderService);


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 这里有个坑需要注意
            // 从根容器获取瞬时服务对象，只有应用程序退出时才会释放，因为根容器会一直持有该对象
            var s = app.ApplicationServices.GetService<IOrderService>();
            var s2 = app.ApplicationServices.GetService<IOrderService>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
