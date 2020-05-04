using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionMiddleware.Demo.Exceptions
{
    public class MyServerException : Exception, IKnownException
    {
        public int ErrorCode { get; private set; }
        public object[] ErrorData { get; private set; }
        public MyServerException(string message, int errorCode, params object[] errorData)
            :base(message)
        {
            this.ErrorCode = errorCode;
            this.ErrorData = errorData;
        }
    }
}
