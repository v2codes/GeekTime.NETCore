作用域与对象释放行为：你知道IDisposable对象释放的时机和坑吗？

作用域，主要是由 IServiceScope 这个接口来承载。

实现 IDisposable 接口类型的释放
	DI 只负责释放由其创建的对象实例
	DI 在容器或子容器释放时，释放由其创建的对象实例

	也就是说，如果是我们自己创建出来的对象，容器是不会负责该对象的管理和释放的。

建议
	避免在根容器获取实现了 IDisposable 接口的瞬时服务
	避免手动创建实现了 IDisposable 的对象放入容器中，应该使用容器来管理其完整的生命周期

IHostApplicationLifetime
	这个接口的作用，是用来管理我们整个应用程序的生命周期
	它有个方法 StopApplication：这个方法可以把我们整个应用程序关闭退出。