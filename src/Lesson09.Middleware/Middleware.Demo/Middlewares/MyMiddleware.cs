using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Demo.Middlewares
{
    public class MyMiddleware
    {
        RequestDelegate _next;
        ILogger _logger;
        public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (_logger.BeginScope("TraceIdentifier:{traceIdentifier}", context.TraceIdentifier))
            {
                _logger.LogDebug("自定义中间件：开始执行");

                // 实现断路器，不执行 _next 方法即可
                await _next(context);

                _logger.LogDebug("自定义中间件：执行结束");
            }
        }
    }
}
