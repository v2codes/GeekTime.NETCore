using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekTime.API.Application.IntegrationEvents
{
    /// <summary>
    /// 领域事件订阅者接口
    /// </summary>
    public interface ISubscriberService
    {
        void OrderPaymentSucceeded(OrderPaymentSucceededIntegrationEvent @event);
        void OrderCreatedSucceeded(OrderCreatedIntegrationEvent @event);
    }
}
