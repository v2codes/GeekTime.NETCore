using GeekTime.Domain.Abstractions;
using GeekTime.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Domain.Events
{
    /// <summary>
    /// 创建Order领域事件
    /// </summary>
    public class OrderCreatedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// 写入私有，读取公开
        /// </summary>
        public Order Order { get; private set; }
        public OrderCreatedDomainEvent(Order order)
        {
            this.Order = order;
        }
    }
}
