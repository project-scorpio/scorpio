---
title: 依赖注入
description: 依赖注入
---

# 依赖注入

## 简单介绍

Scorpio 的依赖注入系统是基于Microsoft的[依赖注入扩展](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)库（Microsoft.Extensions.DependencyInjection nuget包）开发的.因此,它的文档在 Scorpio 中也是有效的。

>Scorpio 框架没有对任何第三方DI提供程序的核心依赖, 但您可以使用一个提供程序来支持动态代理(dynamic proxying)和一些高级特性以便 Scorpio 的部分模块能正常工作。我们已经为您提供了 AspectCore 的实现， 更多信息请参阅 [Aspect](aspect) 文档。

## 模块化注入

因为 Scorpio 是一个模块化框架，因此每个模块都定义它自己的服务并在它自己的单独[模块类](modularity)中通过依赖注入进行注册.例:

``` cs
public class StartupModule:ScorpioModule
{
    public override void ConfigureServices(ConfigureServicesContext context)
    {
        //在这里添加依赖注入代码。
    }
}
```

## 依照约定的注册

Scorpio 引入了依照约定的服务注册.依照约定你只需要调用 `ServicesCollection.RegisterAssemblyByConvention()`{:.language-cs}方法 ,它会自动完成服务的注册。

``` cs
public class StartupModule:ScorpioModule
{
    public override void ConfigureServices(ConfigureServicesContext context)
    {
        context.Services.RegisterAssemblyByConvention();
    }
}
```

### 依赖接口

如果实现这些接口,则会自动将类注册到依赖注入:

+ ITransientDependency 注册为transient生命周期.
+ ISingletonDependency 注册为singleton生命周期.
+ IScopedDependency 注册为scoped生命周期.

示例:

``` cs
public class TaxCalculator : ITransientDependency
{
}
```

`TaxCalculator`{:.language-cs}因为实现了`ITransientDependency`{:.language-cs},所以它会自动注册为transient生命周期。

### ExposeServices 特性

`ExposeServicesAttribute`{:.language-cs}用于控制相关类提供了什么服务及生命周期。例:

``` cs
[ExposeServices(typeof(ITaxCalculator))]
public class TaxCalculator: ICalculator, ITaxCalculator, ICanCalculate, ITransientDependency
{

}
```

`TaxCalculator`{:.language-cs}类只公开`ITaxCalculator`{:.language-cs}接口.这意味着你只能注入`ITaxCalculator`{:.language-cs},但不能注入`TaxCalculator`{:.language-cs}或`ICalculator`{:.language-cs}到你的应用程序中。

### ReplaceService 特性

`ReplaceServiceAttribute`{:.language-cs} 用于控制是否替换之前的注入的服务，使用 `IServiceCollection`{:.language-cs} 的 `Replace`{:.language-cs} 扩展方法。


### 依照约定公开的服务

如果你未指定要公开的服务,则 Scorpio 依照约定公开服务.以上面定义的`TaxCalculator`{:.language-cs}为例:

默认情况下,类本身是公开的。这意味着你可以按`TaxCalculator`{:.language-cs}类注入它。
默认情况下,默认接口是公开的。默认接口是由命名约定确定.在这个例子中,`ICalculator`{:.language-cs}和`ITaxCalculator`{:.language-cs}是`TaxCalculator`{:.language-cs}的默认接口,但`ICanCalculate`{:.language-cs}不是.

## 手动注册

在某些情况下,你可能需要向`IServiceCollection`{:.language-cs}手动注册服务,尤其是在需要使用自定义工厂方法或singleton实例时.在这种情况下,你可以像[Microsoft文档](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)描述的那样直接添加服务.例:

``` cs
public class StartupModule:ScorpioModule
{
    public override void ConfigureServices(ConfigureServicesContext context)
    {
        context.Services.AddSingleton<TaxCalculator>(new TaxCalculator(taxRatio: 0.18));

        context.Services.AddScoped<ITaxCalculator>(sp => sp.GetRequiredService<TaxCalculator>());
    }
}
```

## 第三方提供程序

虽然 Scorpio 框架没有对任何第三方DI提供程序的核心依赖, 但它必须使用一个提供程序来支持动态代理(dynamic proxying)和一些高级特性以便部分 Scorpio 特性能正常工作.

我们已经为您实现了AspectCore的集成.如果您需要使用，您可以在构建 `Bootstrapper`{:.language-cs} 时使用 `BootstrapperCreationOptions.UseAspectCore`{:.language-cs} 方法来集成 AspectCore.

如果您需要集成其他的提供者，您可以实现 `IServiceProviderFactory<TContainerBuilder>`{:.language-cs} 接口并在创建 `Bootstrapper`{:.language-cs} 时使用 `BootstrapperCreationOptions.UseServiceProviderFactory<TContainerBuilder>`{:.language-cs} 方法注入您的提供者.
