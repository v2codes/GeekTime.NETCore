路由与终结点 -- 如何规划好你的Web API

核心作用
    路由的核心作用是用来映射我们的URL和应用程序 Controller 的对应关系
    两个作用
        把URL映射到我们对应的 Controller 和 Action 上
        根据我们的 Controller 和 Action 的名字来生成URL

注册方式
    路由模板方式
        是我们之前传统的方式，可以用来作为我们的MVC的页面Web配置
    RouteAttribute方式
        特性标记的方式，更适合在Web API中使用，现在用的比较多的前后端分离架构，定义Web API的时候，推荐使用RouteAttribute方式

路由约束
    类型约束
    范围约束
    正则表达式
    是否必选
    自定义IRouteConstraint

URL生成
    路由系统提供了2个关键类，用来反向的根据路由信息生成URL地址
    IUrlHelper
        类似于之前MCV项目中的MVCHelper
    LinkGenerator
        全新提供的一个链接生成对象，我们可以在任意位置从容器中获取到这个对象，根据我们的需要生成URL地址  

Web API定义 
    Restful 不是必须的
    约定好 API 的表达契约
    将API约束在特定目录下，如/api/
    使用 ObsoleteAttribute 标记为即将废弃的API，建议使用间隔版本的方式废弃，也就是说，先将我们即将废弃的API标记为已废弃，但它还是可以继续工作，推迟几个版本之后再将代码进行删除。