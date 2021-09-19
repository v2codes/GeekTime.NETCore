using GeekTime.Domain.UserAggregate;
using GeekTime.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Infrastructure.Repositories
{
    /// <summary>
    /// User 仓储实现类
    /// </summary>
    public class UserRepository : Repository<User, long, DomainContext>, IUserRepository
    {
        public UserRepository(DomainContext context)
            : base(context)
        {
        }
    }
}
