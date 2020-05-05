using GeekTime.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Domain.UserAggregate
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class User : Entity<long>, IAggregateRoot
    {
    }
}
