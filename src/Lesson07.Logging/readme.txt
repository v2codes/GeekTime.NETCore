日志框架 -- 聊聊记日志的最佳姿势

    依赖包
        Microsoft.Extensions.Logging
        Microsoft.Extensions.Logging.Console
        Microsoft.Extensions.Logging.Debug
        Microsoft.Extensions.Logging.TraceSource
        
    日志级别
        严重程度从低到高：Trace、Debug、Information、Warning、Error、Critical、None
        值分别是：0~6
        
    日志对象获取
        ILoggerFactory 方式获取
        ILogger<T> 强类型泛型模式获取
        
    日志过滤的配置逻辑    
        根据日志名称，配置日志输出级别、开关等

    日志输出技巧
        在日志文本格式化时，需要注意。
        _logger.LogInformation($"Show Time {DateTime.Now}")：传入方法前，先将日期格式化、拼接字符串
        _logger.LogInformation("Show Time {time}", DateTime.Now)：输出日志时，才会去日期格式化、拼接字符串
        
        采用第二种占位符方式时：当日志设定了高级别，那么低级别日志中会省略格式化字符串步骤，节省资源消耗


日志作用域 -- 解决不同请求之间的日志干扰
    启用作用域
        appsettings中设置 "IncludeScopes": true,
    场景
        一个事务包含多条操作时
        复杂流程的日志关联时
        调用链追踪与请求处理过程对应时

    一般情况下，我们推荐用一个唯一的标识来标识作用域，如 HTTP 请求的Id、Session的Id、会话的Id、事务的Id
    
    Console日志 的内部是异步实现