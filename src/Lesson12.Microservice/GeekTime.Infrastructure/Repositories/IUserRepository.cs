using GeekTime.Domain.UserAggregate;
using GeekTime.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Infrastructure.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository : IRepository<User, long>
    {
    }
}
