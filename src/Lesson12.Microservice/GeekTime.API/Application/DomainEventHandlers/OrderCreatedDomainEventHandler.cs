using DotNetCore.CAP;
using GeekTime.API.Application.IntegrationEvents;
using GeekTime.Domain.Abstractions;
using GeekTime.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeekTime.API.Application.DomainEventHandlers
{
    /// <summary>
    /// 创建Order领域事件处理
    /// </summary>
    public class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        ICapPublisher _capPublisher;
        public OrderCreatedDomainEventHandler(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 领域事件处理
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // 当创建新订单时，向EventBus发布一条集成事件
            await _capPublisher.PublishAsync("OrderCreated", new OrderCreatedIntegrationEvent(notification.Order.Id));
            //return Task.CompletedTask;
        }
    }
}
