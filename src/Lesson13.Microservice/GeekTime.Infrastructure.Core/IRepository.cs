using GeekTime.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekTime.Infrastructure.Core
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        // 当前接口未指定主键类型，所以这里需要根据实体对象去删除
        bool Remove(Entity entity);
        Task<bool> RemoveAsync(Entity entity);
    }

    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键Id类型</typeparam>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : Entity<TKey>, IAggregateRoot
    {
        bool Delete(TKey id);
        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
    }

}
