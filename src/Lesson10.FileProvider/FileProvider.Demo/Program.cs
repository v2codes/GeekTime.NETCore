using Microsoft.Extensions.FileProviders;
using System;

namespace FileProvider.Demo
{
    /// <summary>
    /// 文件提供程序 -- 让你可以将文件放在任何地方
    ///     核心类型
    ///         IFileProvider:用来访问各种文件的提供程序的抽象接口
    ///         IFileInfo:文件信息
    ///         IDirectoryContents:目录信息
    ///     内置提供程序
    ///         PhysicalFileProvider:物理文件提供程序
    ///         EmbeddedFileProvider:嵌入式文件提供程序
    ///         CompositeFileProvider:组合文件提供程序，指当我们有多种文件来源的时候，可以将这些源合并为一个目录，让我们像在使用同一个目录一样使用我们的文件系统
    ///         
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 物理文件
            IFileProvider provider1 = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
            var contents = provider1.GetDirectoryContents("/");
            //foreach (var item in contents)
            //{
            //    // 读取文件流
            //    //var fileStream = item.CreateReadStream();
            //    Console.WriteLine(item.Name);
            //}

            // 嵌入文件
            IFileProvider provider2 = new EmbeddedFileProvider(typeof(Program).Assembly);
            var html = provider2.GetFileInfo("emb.html");
            //var htmlStream = html.CreateReadStream();
            //var buffer = new byte[htmlStream.Length];
            //htmlStream.ReadAsync(buffer, 0, buffer.Length);
            //Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));

            // 组合文件
            IFileProvider provider3 = new CompositeFileProvider(provider1, provider2);

            var contents3 = provider3.GetDirectoryContents("/");

            foreach (var item in contents3)
            {
                // 读取文件流
                //var fileStream = item.CreateReadStream();
                Console.WriteLine(item.Name);
            }

            Console.ReadLine();
        }
    }
}
