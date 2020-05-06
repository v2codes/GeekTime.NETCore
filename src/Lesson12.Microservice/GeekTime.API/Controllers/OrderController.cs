using GeekTime.API.Application.Commands;
using GeekTime.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// 中间者
        /// </summary>
        IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> CreateOrder([FromBody] CreateOrderCommand cmd)
        {
            // 中间者，发送订单创建命令
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }

        ///// <summary>
        ///// 我的订单
        ///// </summary>
        ///// <param name="myOrderQuery"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<List<string>> QueryOrder([FromBody] MyOrderQuery myOrderQuery)
        //{
        //    return await _mediator.Send(myOrderQuery);
        //}

        #region 伪代码，用于讲解 APIController 的最佳实践 --> 不建议的写法
        //[HttpPost]
        //public Task<long> CreateOrder([FromBody]CreateOrderVeiwModel viewModel)
        //{
        //    var model = viewModel.ToModel();
        //    return await orderService.CreateOrder(model);
        //}


        //class OrderService:IOrderService
        //{
        //    public long CreateOrder(CreateOrderModel model)
        //    {
        //        var address = new Address("wen san lu", "hangzhou", "310000");
        //        var order = new Order("xiaohong1999", "xiaohong", 25, address);

        //        _orderRepository.Add(order);
        //        await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        //        return order.Id;
        //    }
        //}
        #endregion
    }

}
