using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekTime.Domain.Abstractions
{
    /// <summary>
    /// 领域事件处理器接口
    /// </summary>
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        //这里我们使用了INotificationHandler的Handle方法来作为处理方法的定义，所以无需重新定义
        //Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
