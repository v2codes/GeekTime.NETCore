选项模式(框架) -- 服务组件集成配置的最佳实践

    命名空间：Microsoft.Extensions.Options
    注册服务时，指定配置（services.Configure<OrderServiceOptions>(Configuration.GetSection("OrderService"));）

特性
    支持单例模式读取配置
    支持快照
    支持配置变更通知
    支持运行时动态修改选项值
    
设计原则
    接口分离原则（ISP），类不应该依赖它不使用的配置
    关注点分离（SoC），不同组件、服务、类之间的配置不应相互依赖或耦合

建议
    为我们的服务设计 XXXOptions 
    使用IOptions<XXXOptions>、IOptionsSnapshot<XXXOptions>、IOptionsMonitor<XXXOptions> 作为服务的构造函数的参数
        

选项数据热更新 -- 让服务感知配置的变化
    关键类
        IOptionsMonitor<out TOptions>
        IOptionsSnapshot<out TOptions>

    使用场景
        范围作用域类型使用 IOptionsSnapshot
        单例服务使用 IOptionsMonitor

    通过代码更新选项
        IPostConfigureOptions<TOptions>
        // 动态更改配置项，配置文件将不会生效
        // 实际上在设计服务的时候，还会有一些特殊诉求，比如把配置读取出来之后还需要在内存里进行一些特殊处理，那么就可以使用动态配置的方式
        services.PostConfigure<OrderServiceOptions>(option =>
        {
            option.MaxOrderCount = 1000;
        });   
            

为选项数据添加验证 -- 避免错误配置的应用接收用户流量
    三种验证方法
        直接注册验证函数
        实现 IValidateOptions<TOptions>
        使用 Microsoft.Extensions.Options.DataAnnotations
    
    通过添加选项验证，可以在配置错误的情况下组织应用程序启动，这样做就可以避免用户流量达到错误的节点（服务）上。