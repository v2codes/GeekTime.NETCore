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
            // 配置选项
            //services.Configure<OrderServiceOptions>(configuration.GetSection("OrderService"));

            // 配置选项：添加验证时，要使用如下方式了，
            services.AddOptions<OrderServiceOptions>().Configure(options =>
            {
                //configuration.Bind(options);
                configuration.GetSection("OrderService").Bind(options);
            })
            // 注册验证函数
            //.Validate(options =>
            //{
            //    return options.MaxOrderCount <= 100;
            //},"Validate Error:MaxOrderCount 不能大于100");

            // 属性验证方式 -- 属性字段增加规则约束，如 [Range(10, 20)]
            //.ValidateDataAnnotations();
            .Services.AddSingleton<IValidateOptions<OrderServiceOptions>, OrderServiceValidateOptions>();

            // 动态配置项，覆盖配置文件中的配置
            // 实际上在设计服务的时候，还会有一些特殊诉求，比如把配置读取出来之后还需要在内存里进行一些特殊处理，那么就可以使用动态配置的方式
            services.PostConfigure<OrderServiceOptions>(option =>
            {
                option.MaxOrderCount += 100;
            });

            services.AddSingleton<IOrderService, OrderService>();
            return services;
        }
    }
}
