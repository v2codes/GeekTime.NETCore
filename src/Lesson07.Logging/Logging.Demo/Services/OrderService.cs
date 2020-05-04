using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Demo.Services
{
    public class OrderService
    {
        private ILogger<OrderService> _logger;
        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;
        }

        public void Show()
        {
            // 以下两种方式，
            // 采用第二种占位符方式时：当日志设定了高级别，那么低级别日志中会省略格式化字符串步骤，节省资源消耗
            // 传入该方法前，先将日期格式化到字符串
            _logger.LogInformation($"Show Time {DateTime.Now}");
            // 输出该日志时，才会将日期格式化到字符串
            _logger.LogInformation("Show Time {time}", DateTime.Now);
        }
    }
}
