using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScopeDisposed.Demo.Services
{
    public interface IOrderService { 
    
    }


    public class DisposableOrderService : IOrderService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"DisposableOrderService Disposed:{this.GetHashCode()}");
        }
    }
}
