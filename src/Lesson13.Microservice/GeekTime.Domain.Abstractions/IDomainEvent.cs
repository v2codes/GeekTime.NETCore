using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace GeekTime.Domain.Abstractions
{
    /// <summary>
    /// 领域事件接口
    /// 用来标记我们某一个对象是否是领域事件
    /// </summary>
    public interface IDomainEvent : INotification
    {
    }
}
