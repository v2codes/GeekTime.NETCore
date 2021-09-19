using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Domain.Abstractions
{
    /// <summary>
    /// 实体抽象类（包含多个主键的实体接口）
    /// </summary>
    public abstract class Entity : IEntity
    {
        public abstract object[] GetKeys();

        public override string ToString()
        {
            return $"[Entity:{GetType().Name}] Keys = {string.Join(",", GetKeys())}";
        }

        #region 领域事件定义处理 DomainEvents

        /// <summary>
        /// 领域事件集合
        /// </summary>
        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// 获取当前实体对象领域事件集合（只读）
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// 添加领域事件至当前实体对象领域事件结合中
        /// </summary>
        /// <param name="eventItem"></param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// 移除指定领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        /// <summary>
        /// 清空所有领域事件
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        #endregion
    }

    /// <summary>
    /// 实体抽象类（包含唯一主键Id的实体接口）
    /// </summary>
    /// <typeparam name="TKey">主键ID类型</typeparam>
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        int? _requestedHasCode;
        public virtual TKey Id { get; protected set; }
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        /// <summary>
        /// 对象是否想等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            Entity<TKey> item = (Entity<TKey>)obj;

            if (item.IsTransient() || this.IsTransient())
            {
                return false;
            }
            else
            {
                return item.Id.Equals(this.Id);
            }
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHasCode.HasValue)
                {
                    _requestedHasCode = this.Id.GetHashCode() ^ 31; // TODO
                }
                return _requestedHasCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        /// <summary>
        /// 对象是否为全新创建的，未持久化的
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id, default);
        }
        public override string ToString()
        {
            return $"[Entity:{GetType().Name}] Id = {Id}";
        }

        /// <summary>
        /// == 操作符重载
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Entity<TKey> left,Entity<TKey> right)
        {
            if (Object.Equals(left,null))
            {
                return (Object.Equals(right, null)) ? true : false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        /// <summary>
        /// != 操作符重载
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
