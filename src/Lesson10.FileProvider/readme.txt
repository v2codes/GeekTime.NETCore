文件提供程序 -- 让你可以将文件放在任何地方

核心类型
    IFileProvider
        用来访问各种文件的提供程序的抽象接口
    IFileInfo
        文件信息
    IDirectoryContents
        目录信息

内置提供程序
    PhysicalFileProvider
        物理文件提供程序
    EmbeddedFileProvider
        嵌入式文件提供程序
    CompositeFileProvider
        组合文件提供程序，指当我们有多种文件来源的时候，可以将这些源合并为一个目录，让我们像在使用同一个目录一样使用我们的文件系统
         