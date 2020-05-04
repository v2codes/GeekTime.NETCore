using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeekTime.API.Application.Queries
{
    /// <summary>
    /// 查询处理类
    /// </summary>
    public class MyOrderQueryHandler : IRequestHandler<MyOrderQuery, List<string>>
    {
        public Task<List<string>> Handle(MyOrderQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<string>());
        }
    }
}
