using DotNetCore.CAP;
using GeekTime.Infrastructure.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeekTime.Infrastructure.Core
{
    /// <summary>
    /// EF上下文
    /// 注：在处理事务的逻辑部分，需要嵌入CAP的代码，构造函数参数 ICapPublisher
    /// </summary>
    public class EFContext : DbContext, IUnitOfWork, ITransaction
    {
        protected IMediator _mediator;

        ICapPublisher _capBus;

        public EFContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus)
            : base(options)
        {
            _mediator = mediator;
            _capBus = capBus;
        }

        #region IUnitOfWork
        /// <summary>
        /// 保存实体变更
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            // 执行发送领域事件 
            await _mediator.DispatchDomainEventsAsync(this);

            return true;
        }

        ///// <summary>
        ///// IUniOfWork中该方法的定义与DbContext中的SaveChangesAsync一致，所以此处无需再进行实现
        ///// </summary>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    return base.SaveChangesAsync();
        //}
        #endregion

        #region ITransaction

        /// <summary>
        /// 当前事务
        /// </summary>
        private IDbContextTransaction _currentTransaction;

        /// <summary>
        /// 公开方法，返回当前私有事务对象
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        /// <summary>
        /// 当前事务是否开启
        /// </summary>
        public bool HasActiveTransaction => _currentTransaction == null;

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return null;
            }
            // 该扩展方法是由CAP组件提供
            // 创建事务时，也要把 ICapPublisher 传入
            // 核心作用是将我们要发送事件逻辑与我们业务的存储都放在同一个事务内部，从而保证事件与业务逻辑的存取都是一致的
            _currentTransaction = Database.BeginTransaction(_capBus, autoCommit: false);

            return Task.FromResult(_currentTransaction);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            if (transaction != _currentTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            }
            try
            {
                // 提交事务之前，安全起见还是要 SaveChanges 一下，保存变更到数据库
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction!=null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public  void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction!=null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        #endregion

    }
}
