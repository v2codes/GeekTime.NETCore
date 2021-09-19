using DotNetCore.CAP;
using GeekTime.Infrastructure.Core.Behaviors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Infrastructure
{
    /// <summary>
    /// 数据库上下文事务处理
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class DomainContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<DomainContext, TRequest, TResponse>
    {
        public DomainContextTransactionBehavior(DomainContext dbContext, ICapPublisher capBus, ILogger<DomainContextTransactionBehavior<TRequest, TResponse>> logger)
            : base(dbContext, capBus, logger)
        {
        }
    }
}
