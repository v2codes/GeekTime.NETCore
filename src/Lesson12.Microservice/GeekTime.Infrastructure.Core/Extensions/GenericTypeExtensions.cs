using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekTime.Infrastructure.Core.Extensions
{
    /// <summary>
    /// Type扩展，获取泛型类型名称
    /// </summary>
    public static class GenericTypeExtensions
    {
        /// <summary>
        /// 获取泛型类型名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGerericTypeName(this Type type)
        {
            var typeName = string.Empty;
            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name)).ToArray();
                typeName = $"{type.Name.Remove(type.Name.IndexOf("`"))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }
            return typeName;
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static string GetGenericTypeName(this object @object)
        {
            return @object.GetType().GetGerericTypeName();
        }
    }
}
