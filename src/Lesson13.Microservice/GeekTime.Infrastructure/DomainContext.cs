using DotNetCore.CAP;
using GeekTime.Domain.OrderAggregate;
using GeekTime.Domain.UserAggregate;
using GeekTime.Infrastructure.Core;
using GeekTime.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace GeekTime.Infrastructure
{
    /// <summary>
    /// EFContext具体实现
    /// </summary>
    public class DomainContext : EFContext
    {
        public DomainContext( DbContextOptions options,IMediator mediator,ICapPublisher capBus)
            :base(options,mediator,capBus)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
