using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routing.Demo.Constraints
{
    /// <summary>
    /// 自定义路由约束
    /// </summary>
    public class MyRouteConstraint : IRouteConstraint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="route"></param>
        /// <param name="routeKey"></param>
        /// <param name="values"></param>
        /// <param name="routeDirection"></param>
        /// <returns></returns>
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (RouteDirection.IncomingRequest == routeDirection)
            {
                var v = values[routeKey];
                if (long.TryParse(v.ToString(), out var value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
