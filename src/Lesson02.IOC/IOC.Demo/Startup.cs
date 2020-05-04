using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOC.Demo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IOC.Demo
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
            #region 注册服务，不同生命周期

            // 单例
            services.AddSingleton<IMySingletonService, MySingletonService>();

            // 作用域
            services.AddScoped<IMyScopedService, MyScopedService>();

            // 瞬时
            services.AddTransient<IMyTransientService, MyTransientService>();
            #endregion

            #region 花式注册
            // 直接注入实例
            services.AddSingleton<IOrderService>(new OrderService());
            //services.AddSingleton<IOrderService, OrderService>();
            
            // 工厂方式注入
            //services.AddSingleton<IOrderService>(serviceProvider =>  
            //{
            //    // 可以使用 serviceProvider 从容器中获取多个对象进行组装来注入需要的服务 
            //    //var testService = serviceProvider.GetService<ITest>();
            //    //return new NeedTestService(testService);
            //    return new OrderServiceEx();
            //});
            #endregion

            #region 尝试注册
            // 如果服务中已经被注册过任一实现，那么就不再注册
            //services.TryAddSingleton<IOrderService, OrderService>();
            //services.TryAddSingleton<IOrderService>(new OrderServiceEx());
            // 当前容器中没有同一具体类型的实现时，才去执行注册该服务
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());
            #endregion

            #region 移除和替换注册
            //services.RemoveAll<IOrderService>();
            //services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());
            #endregion

            #region 注册泛型模板
            services.AddSingleton(typeof(IGerericService<,>), typeof(GenericService<,>));
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
