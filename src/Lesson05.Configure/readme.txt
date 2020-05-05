配置框架：让服务无缝适应各种环境


核心组件包
    Microsoft.Extensions.Configuration.Abstractions
    Microsoft.Extensions.Configuration

配置方式
    通常是以key-value字符串键值对的方式抽象配置（JSON/XML）
    支持从各种不同数据源读取配置(命令行、环境变量、文件等)

核心类型
    IConfiguration:
    IConfigurationRoot:配置的根，所有读取配置项的动作都是在这里完成
    IConfigurationSection:是指配置分组节点，每一节用冒号作为分隔符
    IConfigurationBuilder:构建应用程序配置的核心,所有的设置都在builder中完成

核心扩展点
    IConfigurationSource
    IConfigurationProvider
    通过扩展，指定任意的配置的数据来源来注入到框架
        


命令行参数配置方式 -- 最简单快捷的配置注入方法
    依赖包：Microsoft.Extensions.Configuration.CommandLine

    支持的命令格式
        无前缀 key=value 模式
        双中横线模式 --key=value 或者 --key value
        正斜杠模式 /key=value 或 /key value
        备注：等号分隔符和空格分隔符不能混用
    
    命令替换模式（为命令设置别名）        
        必须以单横线(-)或双划线(--)开头
        映射字典不能包含重复Key
        

环境变量参数配置方式  -- 容器环境下配置注入的最佳途径
    依赖包：Microsoft.Extensions.Configuration.EnvironmentVariables

    适用场景
        在Docker中运行时
        在Kubernetes中运行时
        需要设置ASP.NET Core的一些内置特殊配置时
    特性
        对于配置的分层健，支持用双下划线"__"代替":"
        支持根据前缀加载
    前缀过滤
        是指在注入环境变量的时候，指定一个前缀，就只会注入我们指定前缀的环境变量，不会把所有的环境变量注入进来


环境变量和命令行特点
    在我们早期是没有容器化时，一个操作系统会运行多个应用程序，我们应用程序注入配置的方式一般都是通过文件或者命令行的方式来注入的，命令行的方式使用的也是极少。
    现在，在容器化的环境下，有了Docker的隔离能力，意味着每个应用程序都相当于跑在一个独立的小型操作系统下一样。
    基于Docker提供的环境隔离能力，让我们可以使用环境变量来配置我们的应用程序。
    在Docker、Kubernetes中，我们会大量的使用环境变量，而不是使用命令行来配置我们的基础配置。
        


文件配置提供程序 -- 自由选择配置的格式
    依赖包：读取不同文件格式，或者不同位置来读取配置文件
        Microsoft.Extensions.Configuration.Ini
        Microsoft.Extensions.Configuration.Json
        Microsoft.Extensions.Configuration.NewtonsoftJson
        Microsoft.Extensions.Configuration.Xml
        Microsoft.Extensions.Configuration.UserSecrets

    特性
        文件提供程序支持指定文件可选、必选
        支持指定是否监视文件的变更
        后添加的配置优先级更高，会覆盖前面添加的配置
    

监视配置文件变更 -- 配置热更新能力的核心

    场景
        需要记录配置源变更时
        需要在配置项变更时触发特定操作时

    跟踪配置关键方法
        位于命名空间：Microsoft.Extensions.Primitives;
        IChangeToken token = IConfiguration.GetReloadToken();
        token.RegisterChangeCallback(state=>{
            var root = (ConfigurationRoot)state;
        }, configurationRoot);

        该方式监听只能触发一次，如需持续监视，需要在变更时重新获取 token 并注册变更回调 RegisterChangeCallback
        可以使用 ChangeToken.OnChange()，持续监视配置文件变化
        


配置绑定 -- 使用强类型对象承载配置数据
    引用包：Microsoft.Extensions.Configuration.Binder
    要点
        支持将配置绑定到已有对象
        支持将配置绑定到私有属性上  
     


自定义配置数据源 -- 低成本实现定制化配置方案
    扩展步骤
        实现 IConfigurationSource
        实现 IConfigurationProvider
        实现 AddXXX扩展方法
    