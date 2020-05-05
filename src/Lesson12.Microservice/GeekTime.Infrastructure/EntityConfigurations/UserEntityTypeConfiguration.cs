using System;
using System.Collections.Generic;
using System.Text;
using GeekTime.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekTime.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// 领域模型 User 数据库映射配置
    /// </summary>
    class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            // 指定表名
            builder.ToTable("User");
        }
    }
}
