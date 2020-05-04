using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFac.Demo.Services
{
    public class MyIntercepter : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Intercept before, Method:{invocation.Method.Name}");
            // 执行原有方法逻辑
            invocation.Proceed();
            Console.WriteLine($"Intercept fater, Method:{invocation.Method.Name}");
        }
    }
}
