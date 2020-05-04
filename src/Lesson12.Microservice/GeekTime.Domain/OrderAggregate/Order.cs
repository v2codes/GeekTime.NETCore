using GeekTime.Domain.Abstractions;
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
            //this.AddDomainEvent(new OrderCreatedDomainEvent(this));
        }

        public void ChangeAddress(Address address)
        {
            this.Address = address;

            // 同样的，在修改地址时，也可以定义一个修改地址领域事件
            //this.AddDomainEvent(new OrderAddressChangedDomainEvent(this));
        }
    }
}
