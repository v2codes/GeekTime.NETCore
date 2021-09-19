using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace GeekTime.API.Application.Queries
{
    /// <summary>
    /// 我的订单查询
    /// </summary>
    public class MyOrderQuery : IRequest<List<string>>
    {
        public string UserName { get; set; }
        public MyOrderQuery(string userName)
        {
            UserName = userName;
        }
    }
}
