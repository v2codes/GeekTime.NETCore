using ConfigureConsole.Demo.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace ConfigureConsole.Demo
{
    /// <summary>
    /// 配置框架
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

            // 命令行参数配置方式
            #region 命令行模式
            {
                //var builder = new ConfigurationBuilder();

                //// 命名空间：Microsoft.Extensions.Configuration.CommandLine
                //// 项目属性->调试->应用程序参数。等同于：launchSettings.json 中的 commandLineArgs
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
                //// 项目属性->调试->环境变量。等同于： launchSettings.json 中的 environmentVariables
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
                //// optional:是否可选，默认false即必须有   reloadOnChange:文件变更是否重新加载
                //builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // json
                //builder.AddIniFile("appsettings.ini", optional: false, reloadOnChange: true);// ini

                // 后添加的配置，优先级更高，会覆盖 appsettings.json 中的 key2 配置项
                //builder.AddJsonFile("appsettings.Development");

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
                ////        // 直接访问 configurationRoot对象，或 (ConfigurationRoot)state
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
