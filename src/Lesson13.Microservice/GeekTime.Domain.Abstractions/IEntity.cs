using System;

namespace GeekTime.Domain.Abstractions
{
    /// <summary>
    /// 实体接口（包含多个主键的实体接口）
    /// </summary>
    public interface IEntity
    {
        object[] GetKeys();
    }

    /// <summary>
    /// 实体接口（包含唯一主键Id的实体接口）
    /// </summary>
    /// <typeparam name="TKey">主键ID类型</typeparam>
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }
}
