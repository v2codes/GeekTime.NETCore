using GeekTime.API.Application.IntegrationEvents;
using GeekTime.Domain.OrderAggregate;
using GeekTime.Infrastructure;
using GeekTime.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekTime.API.Extensions
{
    /// <summary>
    /// 服务注册扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 ？？？
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            // 注册事务流程管理类
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainContextTransactionBehavior<,>));

            // package: MediatR.Extensions.Microsoft.Dependency
            return services.AddMediatR(typeof(Order).Assembly, typeof(Program).Assembly);
        }

        /// <summary>
        /// 注册EF上下文
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<DomainContext>(optionsAction);
        }

        /// <summary>
        /// 注册内存EF上下文
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInMemoryDomainContext(this IServiceCollection services)
        {
            // package: Microsoft.EntityFrameworkCore.InMemory
            return services.AddDomainContext(builder => builder.UseInMemoryDatabase("domainContextDatabase"));
        }

        /// <summary>
        /// 注册MySql服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySqlDomainContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDomainContext(builder =>
            {
                // package: Pomelo.EntityFrameworkCore.MySql
                builder.UseMySql(connectionString);
            });
        }

        /// <summary>
        /// 注册仓储服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }

        /// <summary>
        /// 注册EventBus（集成事件处理服务）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            // 注入集成事件订阅服务
            services.AddTransient<ISubscriberService, SubscriberService>();

            // 注入CAP服务
            services.AddCap(options =>
            {
                // 指定CAP组件所使用的数据库上下文，当前设置表示EventBus与领域驱动共享数据库链接
                options.UseEntityFramework<DomainContext>();

                // package: DotNetCore.CAP.RabbitMQ
                // 指定RabbitMQ作为我们EventBus的消息队列的存储，并注入配置
                options.UseRabbitMQ(options =>
                {
                    configuration.GetSection("RabbitMQ").Bind(options);
                });
                // options.UseDashboard();
            });
            return services;
        }

    }
}
