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
            return await _mediator.Send(cmd);
        }

        /// <summary>
        /// 我的订单
        /// </summary>
        /// <param name="myOrderQuery"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<string>> QueryOrder([FromBody] MyOrderQuery myOrderQuery)
        {
            return await _mediator.Send(myOrderQuery);
        }
    }

}
