using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFac.Demo.Services
{
    public interface IMyService
    {
        void ShowCode();
    }
    public class MyService : IMyService
    {
        public void ShowCode()
        {
            Console.WriteLine($"MyService.ShowCode:{this.GetHashCode()}");
        }
    }

    public class MyService2 : IMyService
    {
        public MyNameService nameService { get; set; }
        public void ShowCode()
        {
            Console.WriteLine($"MyService2.ShowCode:{this.GetHashCode()}，NameService是否为空:{nameService == null}");
        }
    }

    public class MyNameService
    {

    }
}
