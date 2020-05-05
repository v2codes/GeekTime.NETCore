using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExceptionMiddleware.Demo.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ExceptionMiddleware.Demo.Controllers
{
    [Route("/error")]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            // 获取当前请求上下文里报出的异常信息
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var ex = exceptionHandlerPathFeature?.Error;
            var knownException = ex as IKnownException;
            if (knownException == null)
            {
                var logger = HttpContext.RequestServices.GetService<ILogger<MyExceptionFilterAttribute>>();
                logger.LogError(ex, ex.Message);
                knownException = KnownException.Unknown;
            }
            else
            {
                knownException = KnownException.FromKnownException(knownException);
            }

            return View(knownException);
        }
    }
}