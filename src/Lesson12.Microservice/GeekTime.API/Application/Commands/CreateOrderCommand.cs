using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekTime.API.Application.Commands
{
    /// <summary>
    /// 创建订单 Command
    /// </summary>
    public class CreateOrderCommand : IRequest<long>
    {
        public long ItemCount { get; private set; }

        // public CreateOrderCommand() { }
        public CreateOrderCommand(int itemCount)
        {
            ItemCount = itemCount;
        }

    }
}
