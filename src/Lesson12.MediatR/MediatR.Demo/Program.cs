using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Demo
{
    /// <summary>
    /// MediaR 实现 CQRS demo
    /// 中介者模式：分离操作命令与命令处理逻辑
    /// 对于 命令处理类的扫描是有顺序的，同一命令不同处理类，后扫描的会覆盖之前扫描的
    /// 对于 领域事件的扫描有顺序的，但同一命令不同处理类，会全部被注册并用于执行处理领域事件
    /// </summary>
    class Program
    {
        async static Task Main(string[] args)
        {
            var services = new ServiceCollection();

            // 注入当前程序集,MediaR框架会扫描当前程序集，获取相关类
            services.AddMediatR(typeof(Program).Assembly);

            // 创建容器
            var serviceProvider = services.BuildServiceProvider();

            // 注入中介者 
            var mediator = serviceProvider.GetService<IMediator>();

            // 发送命令
            await mediator.Send(new MyCommand() { CommandName = "cmd01" });

            // 发送领域事件
            await mediator.Publish(new MyEvent(){EventName="event01"});

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 操作命令
    /// </summary>
    internal class MyCommand : IRequest<long>
    {
        public string CommandName { get; set; }
    }

    /// <summary>
    /// 命令处理类 v2
    /// </summary>
    internal class MyCommandHandlerV2 : IRequestHandler<MyCommand, long>
    {
        public Task<long> Handle(MyCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyCommandHandlerV2 执行命令：{request.CommandName}");
            return Task.FromResult(10L);
        }
    }

    /// <summary>
    /// 命令处理类 v1
    /// </summary>
    internal class MyCommandHandler : IRequestHandler<MyCommand, long>
    {
        public Task<long> Handle(MyCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyCommandHandler 执行命令：{request.CommandName}");
            return Task.FromResult(10L);
        }
    }

    #region 处理领域事件

    // await mediator.Publish(new XXXEvent(){Name="XXX001"});

    /// <summary>
    /// 领域事件
    /// </summary>
    internal class MyEvent : INotification
    {
        public string EventName { get; set; }
    }

    /// <summary>
    /// 领域事件处理类
    /// </summary>
    internal class MyEventHandlerV2 : INotificationHandler<MyEvent>
    {
        public Task Handle(MyEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyEventHandlerV2 执行领域事件：{notification.EventName}");
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// 领域事件处理类
    /// </summary>
    internal class MyEventHandler : INotificationHandler<MyEvent>
    {
        public Task Handle(MyEvent notification,CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyEventHandler 执行领域事件：{notification.EventName}");
            return Task.CompletedTask;
        }
    }

    #endregion
}
