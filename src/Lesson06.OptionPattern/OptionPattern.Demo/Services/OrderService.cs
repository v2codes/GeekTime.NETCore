using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace OptionPattern.Demo.Services
{
    public interface IOrderService
    {
        int ShowMaxOrderCount();
    }
    public class OrderService : IOrderService
    {
        //private IOptions<OrderServiceOptions> _options;
        //// IOptionsSnapshot实现热更新
        //private IOptionsSnapshot<OrderServiceOptions> _options;
        private IOptionsMonitor<OrderServiceOptions> _options;

        public OrderService(IOptionsMonitor<OrderServiceOptions> options)
        {
            _options = options;

            // 配置发生变更，主动通知，注意：调试模式下一次变更会重复通知多次
            _options.OnChange(option =>
            {
                Console.WriteLine($"变更后的选项值：{option.MaxOrderCount}");
            });
        }
        public int ShowMaxOrderCount()
        {
            //return _options.Value.MaxOrderCount;

            return _options.CurrentValue.MaxOrderCount;
        }
    }


    public class OrderServiceOptions
    {
        [Range(10, 20)]
        public int MaxOrderCount { get; set; } = 100;
    }

    public class OrderServiceValidateOptions : IValidateOptions<OrderServiceOptions>
    {
        public ValidateOptionsResult Validate(string name, OrderServiceOptions options)
        {
            if (options.MaxOrderCount > 200)
            {
                return ValidateOptionsResult.Fail("Validate Error:MaxOrderCount不能大于200");
            }
            else
            {
                return ValidateOptionsResult.Success;
            }
        }
    }
}
