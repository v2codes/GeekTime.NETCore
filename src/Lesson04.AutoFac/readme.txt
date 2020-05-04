用Autofac增强容器能力：引入面向切面编程（AOP）的能力

什么情况下需要引入第三方容器组件？
	基于名称的注入
	属性注入
	子容器
	基于动态代理的 AOP

.NET Core 依赖注入框架核心扩展点
	public interface IServiceProviderFactory<TContainerBuilder>

	第三方依赖注入框架，都是使用了这个类来作为扩展点，把自己注入到.NET Core的框架里面来。
	也就是说，我们在使用第三方依赖注入框架时，不需要关注各自的功能、接口等，我们只需要使用官方核心的定义就可以正常使用。

Autofac
	安装引入：Autofac.Extensions.DependencyInjection、Autofac.Extras.DynamicProxy	
	引入上述两个包，就可以达到前面所需要的四中能力

Program.cs 中
	UseServiceProviderFactory(new AutofacServiceProviderFactory)，它就是用来注册第三方容器的入口

