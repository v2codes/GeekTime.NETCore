using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOC.Demo.Services
{
    public interface IGerericService<T, K>
    {

    }
    public class GenericService<T, K> : IGerericService<T, K>
    {
        public T Data { get; set; }
        public GenericService(T data)
        {
            Data = data;
        }
    }
}
