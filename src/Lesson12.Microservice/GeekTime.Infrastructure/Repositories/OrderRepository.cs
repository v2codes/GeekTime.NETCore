using GeekTime.Domain.OrderAggregate;
using GeekTime.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Infrastructure.Repositories
{
    /// <summary>
    /// Order 仓储实现类
    /// </summary>
    public class OrderRepository : Repository<Order, long, DomainContext>, IOrderRepository
    {
        public OrderRepository(DomainContext context)
            : base(context)
        {
        }
    }
}
