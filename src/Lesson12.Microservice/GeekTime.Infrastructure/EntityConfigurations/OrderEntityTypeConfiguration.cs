using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeekTime.Domain.OrderAggregate;

namespace GeekTime.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// 领域模型 Order 数据库映射配置
    /// </summary>
    class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // 定义主键
            builder.HasKey(p => p.Id);

            // 指定表名
            //builder.ToTable("Order");

            // 设置字段长度限制
            //builder.Property(p => p.UserId).HasMaxLength(20);
            //builder.Property(p => p.UserName).HasMaxLength(30);

            // 导航属性
            builder.OwnsOne(c => c.Address, a =>
            {
                a.WithOwner();

                //a.Property(p => p.City).HasMaxLength(20);
                //a.Property(p => p.Street).HasMaxLength(50);
                //a.Property(p => p.ZipCode).HasMaxLength(10);
            });
        }
    }
}
