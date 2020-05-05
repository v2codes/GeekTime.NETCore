using Microsoft.Extensions.FileProviders;
using System;

namespace FileProvider.Demo
{
    /// <summary>
    /// 文件提供程序 -- 让你可以将文件放在任何地方
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
