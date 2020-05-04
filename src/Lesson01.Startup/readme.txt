Startup -- 掌握ASP.NET Core的启动过程

	Program.cs
		main函数调用了下面的 CreateHostBuilder 方法，CreateHostBuilder 返回值为 IHostBuilder
		
		IHostBuilder	
			应用程序初始化抽象接口，是我们应用程序启动的核心接口


	启动过程（Program、Startup类）：
		1）ConfigureWebHostDefaults
			第一阶段，这个阶段注册了应用程序必要的几个组件，比如：配置组件、容器组件
		2）ConfigureHostConfiguration
			第二阶段，用来配置应用程序启动时必要的配置，比如：应用程序启动时需要监听的端口、URL地址，这个过程中我们可以嵌入我们自己的配置内容，注入到配置框架中去，
		3）ConfigureAppConfiguration
			第三阶段，是让我们来嵌入我们自己的配置文件供应用程序读取，该配置将来就会在应用程序执行过程中间供每个组件读取
		4）ConfigureServices --> ConfigureLogging --> Startup --> Startup.ConfigureServices
			第四阶段，这些方法，都是用来往容器里注入我们的应用的组件
		5）Startup.Configure
			第五阶段，用来注入我们自己的中间件，处理HttpContext整个的请求过程的
		
		
	Startup类，在应用程序启动过程是非必须的，只是为了让我们的代码结构更合理。
		以下方法同Startup类作用一致，在Program类的 CreateHostBuilder 方法中：
		webBuilder.ConfigureServices(...);
		webBuilder.Configure(...);

	Startup.ConfigureServices
		通常是在这里注册我们所需要的服务，格式类似：services.AddXXX()。

	Startup.Configure
		这里通常是用来决定我们要注册哪些中间件到处理过程中去。