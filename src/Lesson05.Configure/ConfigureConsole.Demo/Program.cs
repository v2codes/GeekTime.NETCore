using ConfigureConsole.Demo.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace ConfigureConsole.Demo
{
    /// <summary>
    /// 配置框架 -- 让服务无缝适应各种环境
    ///     配置方式
    ///         通常是以key-value字符串键值对的方式抽象配置（JSON/XML）
    ///         支持从各种不同数据源读取配置(命令行、环境变量、文件等)
    ///     核心组件包
    ///         Microsoft.Extensions.Configuration.Abstractions
    ///         Microsoft.Extensions.Configuration
    ///     核心类型
    ///         IConfiguration:
    ///         IConfigurationRoot:配置的根，所有读取配置项的动作都是在这里完成
    ///         IConfigurationSection:是指配置分组节点，每一节用冒号作为分隔符
    ///         IConfigurationBuilder:构建应用程序配置的核心,所有的设置都在builder中完成
    ///     核心扩展点（通过扩展，指定任意的配置的数据来源来注入到框架）
    ///         IConfigurationSource
    ///         IConfigurationProvider
    ///         
    /// 命令行参数配置方式 -- 最简单快捷的配置注入方法
    ///     引用包：Microsoft.Extensions.Configuration.CommandLine
    ///     无前缀 key=value 模式
    ///     双中横线模式 --key=value 或者 --key value
    ///     正斜杠模式 /key=value 或 /key value
    ///     备注：等号分隔符和空格分隔符不能混用
    ///     
    ///     命令替换模式
    ///         必须以单横线(-)或双划线(--)开头
    ///         映射字典不能包含重复Key
    ///         
    /// 环境变量参数配置方式  -- 容器环境下配置注入的最佳途径
    ///     引用包：Microsoft.Extensions.Configuration.EnvironmentVariables
    ///     适用场景
    ///         在Docker中运行时
    ///         在Kubernetes中运行时
    ///         需要设置ASP.NET Core的一些内置特殊配置时
    ///     特性
    ///         对于配置的分层健，支持用双下划线"__"代替":"
    ///         支持根据前缀加载
    ///         
    /// 文件配置提供程序 -- 自由选择配置的格式
    ///     引用包：读取不同文件格式，或者不同位置来读取配置文件
    ///         Microsoft.Extensions.Configuration.Ini
    ///         Microsoft.Extensions.Configuration.Json
    ///         Microsoft.Extensions.Configuration.NewtonsoftJson
    ///         Microsoft.Extensions.Configuration.Xml
    ///         Microsoft.Extensions.Configuration.UserSecrets
    ///     特性
    ///         文件提供程序支持指定文件可选、必选
    ///         支持指定是否监视文件的变更
    ///     
    ///     监视配置文件变更 -- 配置热更新能力的核心
    ///         IChangeToken IConfiguration.GetReloadToken()
    ///         场景：需要记录配置源变更、需要在配置项变更时触发特定操作时
    ///         只能触发一次，如需持续监视，需要在变更时重新获取 token 并注册变更回调 RegisterChangeCallback
    ///         使用 ChangeToken.OnChange()，持续监视配置文件变化
    ///         
    /// 配置绑定 -- 使用强类型对象承载配置数据
    ///     引用包：Microsoft.Extensions.Configuration.Binder
    ///     要点
    ///         支持将配置绑定到已有对象
    ///         支持将配置绑定到私有属性上  
    ///      
    /// 自定义配置数据源 -- 低成本实现定制化配置方案
    ///     扩展步骤
    ///         实现 IConfigurationSource
    ///         实现 IConfigurationProvider
    ///         实现 AddXXX扩展方法
    ///     
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 内存字典配置方式
            #region 内存字典模式
            {
                //IConfigurationBuilder builder = new ConfigurationBuilder();
                //// 注入内存配置字典
                //builder.AddInMemoryCollection(new Dictionary<string, string>()
                //{
                //    {"key1","value1" },
                //    {"key2","value2" },
                //    {"section1:key4","value4" }, // 配置分组节点，每一节用冒号作为分隔符 
                //    {"section2:key5","value5" },
                //    {"section2:section2_2:key6","value6" },// 嵌套节点
                //});

                //IConfigurationRoot configurationRoot = builder.Build();

                //Console.WriteLine($"key1={configurationRoot["key1"]}");
                //Console.WriteLine($"key2={configurationRoot["key2"]}");

                //IConfiguration configuration = configurationRoot;

                //IConfigurationSection section = configuration.GetSection("section1");
                //Console.WriteLine($"section1:key4={section["key4"]}");

                //IConfigurationSection section2 = configuration.GetSection("section2");
                //Console.WriteLine($"section2:key5={section2["key5"]}");

                //IConfigurationSection section2_2 = section2.GetSection("section2_2");
                //Console.WriteLine($"section2_2:key6={section2_2["key6"]}");
            }
            #endregion

            #region 命令替换模式
            {
                //var builder = new ConfigurationBuilder();

                //// 命名空间：Microsoft.Extensions.Configuration.CommandLine
                //// 等同于 launchSettings.json 中的 commandLineArgs
                //// builder.AddCommandLine(args);

                //// 命令替换 
                //// 命令映射， -k1 映射到 CommandLineKey1
                //// - 给我们的应用的命令行参数提供了一个短命名快捷命名的方式，例如 -h 替换 --help
                //var mapper = new Dictionary<string, string> { { "-k1", "CommandLineKey1" } };
                //builder.AddCommandLine(args, mapper);

                //var configurationRoot = builder.Build();
                //Console.WriteLine($"CommandLineKey1={configurationRoot["CommandLineKey1"]}");
                //Console.WriteLine($"CommandLineKey2={configurationRoot["CommandLineKey2"]}");
            }
            #endregion

            // 环境变量配置提供程序
            #region 环境变量配置方式
            {
                //var builder = new ConfigurationBuilder();

                //// 命名空间：Microsoft.Extensions.Configuration.EnvironmentVariables
                //// 等同于 launchSettings.json 中的 environmentVariables
                //builder.AddEnvironmentVariables();

                //var configurationRoot = builder.Build();
                //Console.WriteLine($"key1:{configurationRoot["key1"]}");

                //// 分层健
                //var section = configurationRoot.GetSection("SECTION1");
                //Console.WriteLine($"SECTION1.key3:{section["KEY3"]}");

                //// 多级分层
                //var section2 = section.GetSection("SECTION2");
                ////Console.WriteLine($"SECTION2.key4:{section2["KEY4"]}");

                //// 根节点直接获取
                //// 注意此处，节点层级之间的冒号":"，代表实际配置中的双下划线"__"
                //var section2_2 = configurationRoot.GetSection("SECTION1:SECTION2");
                //Console.WriteLine($"SECTION2.key4:{section2_2["KEY4"]}");
            }
            #endregion

            #region 前缀过滤 -- 只会注入指定前缀的环境变量，不会把所有的环境变量注入进来
            {
                //var builder = new ConfigurationBuilder();
                //builder.AddEnvironmentVariables("XIAO_");
                //var configurationRoot = builder.Build();
                //Console.WriteLine($"KEY1:{configurationRoot["KEY1"]}");
                //Console.WriteLine($"KEY2:{configurationRoot["KEY2"]}"); // 未被注入，获取不到该配置项
            }
            #endregion


            // 文件配置提供程序
            #region 文件配置
            {
                //var builder = new ConfigurationBuilder();
                //// optional:是否可选，默认false即必须有
                //// reloadOnChange:文件变更是否重新加载
                //builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // json
                //builder.AddIniFile("appsettings.ini", optional: false, reloadOnChange: true);// ini

                //var configurationRoot = builder.Build();

                //Console.WriteLine($"key1:{configurationRoot["Key1"]}");
                //Console.WriteLine($"key2:{configurationRoot["Key2"]}");
                //Console.WriteLine($"key5:{configurationRoot["Key5"]}");
                //Console.WriteLine($"key6:{configurationRoot["Key6"]}");
                //Console.WriteLine($"ini key3:{configurationRoot["Key3"]}");

                //Console.WriteLine($"请先修改appsettings.json文件，输入任意键重新加载配置！");
                //Console.ReadKey();
                //Console.WriteLine($"key1:{configurationRoot["Key1"]}");
                //Console.WriteLine($"key2:{configurationRoot["Key2"]}");
                //Console.WriteLine($"key5:{configurationRoot["Key5"]}");
                //Console.WriteLine($"key6:{configurationRoot["Key6"]}");

                //Console.WriteLine($"ini key3:{configurationRoot["Key3"]}");
            }
            #endregion

            #region 监视配置文件变更
            {
                //var builder = new ConfigurationBuilder();
                //builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // json

                //var configurationRoot = builder.Build();

                //IChangeToken token = configurationRoot.GetReloadToken();

                //// 持续注册监听回调
                ////token.RegisterChangeCallback(state =>
                ////{
                ////    var root = (ConfigurationRoot)state;
                ////    Console.WriteLine("配置文件变更：");
                ////    Console.WriteLine($"key1:{root["Key1"]}");
                ////    Console.WriteLine($"key2:{root["Key2"]}");
                ////    Console.WriteLine($"key5:{root["Key5"]}");
                ////    Console.WriteLine($"key6:{root["Key6"]}");

                ////    token = root.GetReloadToken();
                ////    token.RegisterChangeCallback(state =>
                ////    {
                ////        var root = (ConfigurationRoot)state;
                ////        Console.WriteLine("配置文件变更：");
                ////        Console.WriteLine($"key1:{root["Key1"]}");
                ////        Console.WriteLine($"key2:{root["Key2"]}");
                ////        Console.WriteLine($"key5:{root["Key5"]}");
                ////        Console.WriteLine($"key6:{root["Key6"]}");
                ////    }, root);
                ////}, configurationRoot);

                //ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), state =>
                //{
                //    var root = (ConfigurationRoot)state;
                //    Console.WriteLine("配置文件变更：");
                //    Console.WriteLine($"key1:{root["Key1"]}");
                //    Console.WriteLine($"key2:{root["Key2"]}");
                //    Console.WriteLine($"key5:{root["Key5"]}");
                //    Console.WriteLine($"key6:{root["Key6"]}");
                //}, configurationRoot);

                //Console.WriteLine($"key1:{configurationRoot["Key1"]}");
                //Console.WriteLine($"key2:{configurationRoot["Key2"]}");
                //Console.WriteLine($"key5:{configurationRoot["Key5"]}");
                //Console.WriteLine($"key6:{configurationRoot["Key6"]}");
            }
            #endregion

            #region 绑定配置到强类型对象
            {
                //var builder = new ConfigurationBuilder();
                //builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // json
                //var configurationRoot = builder.Build();

                //var config = new Config()
                //{
                //    Key1 = "config key1",
                //    Key5 = true,
                //    Key6 = 111,
                //};
                //// 配置文件绑定到已有对象
                //configurationRoot.Bind(config, binderOptions =>
                // {
                //     // 设置绑定私有属性
                //     binderOptions.BindNonPublicProperties = true;
                // });

                //// 绑定嵌套节点
                //configurationRoot.GetSection("OrderService").Bind(config);
                //Console.WriteLine($"key1:{config.Key1}");
                //Console.WriteLine($"key5:{config.Key5}");
                //Console.WriteLine($"key6:{config.Key6}");
                //Console.WriteLine($"key7:{config.Key7}");
                //Console.WriteLine($"key8:{config.Key8}");
                //Console.WriteLine($"key9:{config.Key9}");
                //Console.WriteLine($"key10:{config.Key10}");
            }
            #endregion

            // 自定义配置数据源 
            #region 自定义配置数据源
            {
                var builder = new ConfigurationBuilder();
                //builder.Add(new MyConfigurationSource());
                builder.AddMyConfiguration();

                var configurationRoot = builder.Build();

                Console.WriteLine($"lastTime:{configurationRoot["lastTime"]}");
                ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), state =>
                  {
                      var root = (ConfigurationRoot)state;
                      Console.WriteLine($"lastTime:{root["lastTime"]}");
                  }, configurationRoot);
            }
            #endregion

            Console.ReadLine();
        }
    }

    public class Config
    {
        public string Key1 { get; set; }
        public bool Key5 { get; set; }
        public int Key6 { get; set; }

        // OrderService 节点配置
        public string Key7 { get; set; }
        public string Key8 { get; set; }
        public bool Key9 { get; set; }
        public string Key10 { get; private set; } = "private default value";
    }
}
