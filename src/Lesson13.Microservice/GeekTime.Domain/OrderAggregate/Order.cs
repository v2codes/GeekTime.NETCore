using GeekTime.Domain.Abstractions;
using GeekTime.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Domain.OrderAggregate
{
    /// <summary>
    /// 订单实体
    /// </summary>
    public class Order : Entity<long>, IAggregateRoot
    {
        // 实体内字段的 set 方法都是 private 的
        // 实体类型相关的数据操作，都应该是由我们实体来负责，而不是被外部的对象去操作
        // 这样的好处是让我们的领域模型符合封闭开放的原则

        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public Address Address { get; private set; }
        public int ItemCount { get; set; }

        protected Order()
        {
        }

        public Order(string userId, string userName, int itemCount, Address address)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.ItemCount = itemCount;
            this.Address = address;

            // 构造新的Order对象的时候，添加一个创建Order领域事件
            this.AddDomainEvent(new OrderCreatedDomainEvent(this));
        }

        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name="address"></param>
        public void ChangeAddress(Address address)
        {
            this.Address = address;

            // 同样的，在修改地址操作时，也该定义一个类似的修改地址领域事件
            //this.AddDomainEvent(new OrderAddressChangedDomainEvent(this));
        }
    }
}
