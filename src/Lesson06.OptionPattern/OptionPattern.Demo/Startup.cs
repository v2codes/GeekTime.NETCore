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
using OptionPattern.Demo.Services;

namespace OptionPattern.Demo
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
            #region 选项模式
            // 服务注册
            ////services.AddSingleton<OrderServiceOptions>();
            //services.Configure<OrderServiceOptions>(Configuration.GetSection("OrderService"));
            //services.AddSingleton<IOrderService, OrderService>();
            #endregion

            #region 作用域：选项数据热更新 -- IOptionsSnapshot
            // 在需要注入的地方，使用 IOptionsSnapshot<OrderServiceOptions> 即可
            //services.Configure<OrderServiceOptions>(Configuration.GetSection("OrderService"));
            //services.AddScoped<IOrderService, OrderService>();
            #endregion

            #region 单例：选项数据热更新 -- IOptionsMonitor
            // 在需要注入的地方，使用 IOptionsMonitor<OrderServiceOptions> 即可
            //services.Configure<OrderServiceOptions>(Configuration.GetSection("OrderService"));
            //services.AddSingleton<IOrderService, OrderService>();
            #endregion

            #region 使用扩展方法，简化服务注册逻辑
            services.AddOrderService(Configuration);
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
