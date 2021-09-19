using GeekTime.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekTime.Domain.OrderAggregate
{
    /// <summary>
    /// 地址实体
    /// 定义为值对象
    /// </summary>
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public Address()
        {

        }
        public Address(string street, string city, string zipCode)
        {
            this.Street = street;
            this.City = city;
            this.ZipCode = zipCode;
        }

        /// <summary>
        /// 重载获取原子值的方法
        /// 这里特殊的是，我们使用了 yield return 的方式
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return ZipCode;
        }
    }
}
