using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExceptionMiddleware.Demo.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExceptionMiddleware.Demo
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
            #region  3. 过滤器/特性处理异常方式，只能用在MVC、WEBAPI 处理过程中
            services.AddMvc(mvcOptions =>
            {
                // 过滤器方式
                //mvcOptions.Filters.Add<MyExceptionFilter>();
                // 特性方式
                mvcOptions.Filters.Add<MyExceptionFilterAttribute>();
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    // 开发环境下，才会使用该错误页，生产环境必须关闭
            //    app.UseDeveloperExceptionPage();
            //}

            #region 1. 定义错误处理程序(Controller/Action)，需要 services.AddMvc();
            //app.UseExceptionHandler("/error");
            #endregion

            #region 2. 使用匿名委托
            //app.UseExceptionHandler(errApp =>
            //{
            //    errApp.Run(async context =>
            //    {
            //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            //        var ex = exceptionHandlerPathFeature?.Error;
            //        var knownException = ex as IKnownException;
            //        if (knownException == null)
            //        {
            //            var logger = context.RequestServices.GetService<ILogger<MyExceptionFilterAttribute>>();
            //            logger.LogError(ex, ex.Message);
            //            knownException = KnownException.Unknown;

            //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        }
            //        else
            //        {
            //            knownException = KnownException.FromKnownException(knownException);

            //            context.Response.StatusCode = StatusCodes.Status200OK;
            //        }
            //        var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
            //        context.Response.ContentType = "application/json; charset=utf-8";
            //        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knownException, jsonOptions.Value.JsonSerializerOptions));
            //    });
            //});
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
