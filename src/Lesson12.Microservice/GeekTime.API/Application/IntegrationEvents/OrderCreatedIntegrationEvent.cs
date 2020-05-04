using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekTime.API.Application.IntegrationEvents
{
    /// <summary>
    /// 集成事件：订单创建
    /// </summary>
    public class OrderCreatedIntegrationEvent
    {
        public long OrderId { get; }
        public OrderCreatedIntegrationEvent(long orderId)
        {
            OrderId = orderId;
        }
    }
}
