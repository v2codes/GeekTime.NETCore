using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOC.Demo.Services
{
    public interface IOrderService { }
    public class OrderService : IOrderService
    {
    }
    public class OrderServiceEx : IOrderService
    {

    }
}
