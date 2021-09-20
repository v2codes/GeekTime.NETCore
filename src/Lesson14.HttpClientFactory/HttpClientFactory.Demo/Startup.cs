using HttpClientFactory.Demo.Clients;
using HttpClientFactory.Demo.DelegatingHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientFactory.Demo
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
            // 1. 注册 HttpClientFactory
            services.AddMvc().AddControllersAsServices();
            services.AddHttpClient();
            services.AddScoped<OrderClient>();

            // 2. 命名客户端模式
            // 2.1 命名客户端模式
            //services.AddHttpClient("namedOrderClient", client =>
            //{
            //    client.DefaultRequestHeaders.Add("client-name", "namedclient");
            //    client.BaseAddress = new Uri("https://localhost:5004");
            //})；
            //services.AddScoped<NamedOrderClient>();

            // 2.2 自定义生命周期时间、自定义请求处理程序
            services.AddSingleton<RequestIdDelegatingHandler>();
            services.AddHttpClient("namedOrderClient", client =>
            {
                client.DefaultRequestHeaders.Add("client-name", "namedclient");
                client.BaseAddress = new Uri("https://localhost:5004");
            })
             .SetHandlerLifetime(TimeSpan.FromMinutes(2)) // 设置当前 HttpClient 中 Handler 的生命周期
             .AddHttpMessageHandler(serviceProvicer => serviceProvicer.GetService<RequestIdDelegatingHandler>());
            services.AddScoped<NamedOrderClient>();


            // 3. 类型化客户端模式
            services.AddHttpClient<TypedOrderClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5004");
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HttpClientFactory.Demo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HttpClientFactory.Demo v1"));
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
