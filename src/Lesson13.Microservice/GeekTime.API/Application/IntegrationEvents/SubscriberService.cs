using DotNetCore.CAP;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekTime.API.Application.IntegrationEvents
{
    /// <summary>
    /// 集成事件订阅服务
    /// </summary>
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        IMediator _mediator;
        public SubscriberService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 订阅订单创建成功集成事件
        /// </summary>
        /// <param name="event"></param>
        [CapSubscribe("OrderCreated")]
        public void OrderCreatedSucceeded(OrderCreatedIntegrationEvent @event)
        {
            // do something...
        }

        /// <summary>
        /// 订阅订单支付成功集成事件
        /// </summary>
        /// <param name="event"></param>
        [CapSubscribe("OrderPaymentSucceeded")]
        public void OrderPaymentSucceeded(OrderPaymentSucceededIntegrationEvent @event)
        {
            // do something...
        }
    }
}
