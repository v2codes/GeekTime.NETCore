namespace ExceptionMiddleware.Demo.Exceptions
{
    public interface IKnownException
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// 异常
        /// </summary>
        public object[] ErrorData { get; }
    }
}