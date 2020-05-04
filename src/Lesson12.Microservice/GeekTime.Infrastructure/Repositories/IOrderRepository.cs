using GeekTime.Domain.OrderAggregate;
using GeekTime.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Infrastructure.Repositories
{
    /// <summary>
    /// Order 仓储接口
    /// </summary>
    public interface IOrderRepository : IRepository<Order, long>
    {
    }
}
