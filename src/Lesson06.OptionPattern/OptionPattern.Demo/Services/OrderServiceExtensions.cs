using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionPattern.Demo.Services
{
    public static class OrderServiceExtensions
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<OrderServiceOptions>(configuration.GetSection("OrderService"));

            services.AddOptions<OrderServiceOptions>().Configure(options =>
            {
                //configuration.Bind(options);
                configuration.GetSection("OrderService").Bind(options);
            })
            // 注册验证函数
            //.Validate(options =>
            //{
            //    return options.MaxOrderCount < 100;
            //},"Validate Error:MaxOrderCount 不能大于100");

            // 属性验证方式 -- 字段属性增加规则约束，如 [Range(10, 20)]
            //.ValidateDataAnnotations();
            .Services.AddSingleton<IValidateOptions<OrderServiceOptions>, OrderServiceValidateOptions>();

            // 动态配置项，覆盖配置文件中的配置
            services.PostConfigure<OrderServiceOptions>(option =>
            {
                option.MaxOrderCount = 1000;
            });

            services.AddSingleton<IOrderService, OrderService>();
            return services;
        }
    }
}
