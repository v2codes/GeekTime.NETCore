using DotNetCore.CAP;
using GeekTime.Domain.OrderAggregate;
using GeekTime.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeekTime.API.Application.Commands
{
    /// <summary>
    /// 领域事件:订单创建命令处理程序
    /// 注：在创建完我们的领域模型并将其保存之后才会触发该处理程序
    /// </summary>
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, long>
    {
        IOrderRepository _orderRepository;
        ICapPublisher _capPublisher;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICapPublisher capPublisher)
        {
            _orderRepository = orderRepository;
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 处理订单创建命令
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Address("wen san lu", "hangzhou", "310000");
            var order = new Order("xiaohong1999", "小红", (int)request.ItemCount, address);

            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return order.Id;
        }
    }
}
