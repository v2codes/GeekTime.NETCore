中间件 -- 掌控请求处理过程的关键

工作原理：（俄罗斯套娃）
                 Middleware1          Middleware2         Middleware3     
    Request ->    //logic
                    next();    ->      //logic
                                        next();     ->      //logic
                                                      
                                                            //more logic
                                       //more logic  <- 
                    //more logic  <-
    Response <-

    中间件的工作原理，如上图所示，顺时针旋转90°的话，就像是俄罗斯套娃，Middleware1 是最大的套娃，其他中间件一个一个嵌套下去。
    next()，就表示后面的所有套娃的一个委托，我们可以在任意的中间件里来决定在后面的中间件之前执行什么，或者是在所有中间件执行完成之后执行什么     

核心对象
    IApplicationBuilder
        注册中间件

        Use(Func<RequestDelegate,RequestDelegate> middleware)
            用于注册中间件，每个委托的入参也是一个委托，意味着我们可以把这些委托注册成一个链
        Build()
            最终执行Build，返回一个委托，这个委托就是把所有中间件串起来之后，合并成的一个委托方法

    RequestDelegate
        处理整个请求的委托
            
自定义中间件
    采用约定的方式，符合下述条件，就可以注册为中间件并被框架识别启用
    类型中包含一个 Invoke/InvokeAsync方法，返回值为Task，入参是 HttpContext

在使用中间件过程中，一定要注意注册中间件的顺序，这个顺序决定了我们中间件执行的时机，某些中间件会是断路器的作用，某些中间件会做一些请求内容的处理。
我们应用程序一旦开始向 Response write的时候，后续的中间件就不能再去操作它的 header，可以通过 Context.Resposne.HasStarted 来判断，我们是否已经开始向相应的body输出内容。



异常处理中间件 -- 区分真异常与逻辑异常
    异常处理方式
        异常处理页
        异常处理匿名委托方法
        IExceptionFilter
        ExceptionFilterAttribute0

    异常处理技巧
        用特定的异常类或接口表示业务逻辑异常
        为业务逻辑异常定义全局错误码
        为未知异常定义特定的输出信息和错误码
        对于已知业务逻辑异常响应 HTTP 200（监控系统友好）
        对于未预见的异常响应 HTTP 500
        为所有的异常记录详细的日志



静态文件中间件 -- 前后端分离开发合并部署骚操作

    能力
        支持指定相对路径
        支持目录浏览:services.AddDirectoryBrowser(); app.UseDirectoryBrowser();
        支持设置默认文档:app.UseDefaultFiles();
        支持多目录映射
    
    app.UseStaticFiles();
        启动静态文件中间件。当我们需要使用中间件静态文件输出的时候，首选的应该是把静态文件放在 wwwroot 目录内。
 
    小任务：实现前端HTML5 History 路由模式支持（配置除了api之外的请求，都响应index.html）
        一般情况下，我们的项目都是前后端分离的架构，前端会编译成一个index.html文件、若干个css文件、若干JavaScript文件、若干图片文件等等。
        css、JavaScript、图片文件一般情况下都是部署在CDN服务器上。
        index.html文件，就需要我们建立一个宿主来 host 它，并且前端路由的话一般都会用 HTML5 的 History 路由模式。
        此时，前端就会对后端有一个特殊的诉求，除了API请求意外，其他请求都应该响应的是 index.html 这个静态文件。
        要达到这个目的，我们就可以借助中间件的执行原理去实现。

        
        
        
 