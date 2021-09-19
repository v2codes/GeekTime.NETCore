using DotNetCore.CAP;
using GeekTime.Infrastructure.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekTime.Infrastructure.Core.Behaviors
{
    /// <summary>
    /// 注入事务管理过程
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class TransactionBehavior<TDbContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TDbContext : EFContext
    {
        ILogger _logger;
        TDbContext _dbContext;
        ICapPublisher _capBus;

        public TransactionBehavior(TDbContext dbContext, ICapPublisher capBus, ILogger logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException();
            _capBus = capBus ?? throw new ArgumentNullException(nameof(capBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 事务执行
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                // 判断当前是否有开启事务，如果开启就执行后续动作
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                // 数据库操作默认执行策略
                // 比如，可以嵌入重试逻辑
                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    // 开启事务
                    Guid transactionId;
                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    // 记录开启的事务
                    using (_logger.BeginScope("TransactionContext:{TransactionId}", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- 开始事务 {TransactionId} ({@Command})", transaction.TransactionId, typeName, request);

                        // 类似中间件模式，后续逻辑执行完成后，提交事务
                        response = await next();

                        _logger.LogInformation("----- 提交事务 {TransactionId} ({CommandName})", transaction.TransactionId, typeName);

                        // 提交事务
                        await _dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;

                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理事务出错 {CommandName} ({@Command})", typeName, request);
                throw;
            }

        }
    }
}
