using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekTime.API.Application.IntegrationEvents
{
    /// <summary>
    /// 集成事件：订单支付成功
    /// </summary>
    public class OrderPaymentSucceededIntegrationEvent
    {
        public long OrderId { get; }
        public OrderPaymentSucceededIntegrationEvent(long orderId)
        {
            OrderId = orderId;
        }
    }
}
