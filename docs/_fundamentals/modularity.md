---
title: Scorpio 基础知识
description: Scorpio 基础知识
---

# 模块化

## 介绍

Scorpio 本身是一个包含许多nuget包的模块化框架.它提供了一个完整的基础架构来开发你自己的应用程序模块。

## ScorpioModule

每个模块都应该定义一个模块类.定义模块类的最简单方法是创建一个派生自 `ScorpioModule`{:.language-cs} 类,如下所示:

``` cs
public class StartupModule:ScorpioModule
{

}
```

## 配置依赖注入

### ConfigureServices方法

`ConfigureServices`{:.language-cs} 是将你的服务添加到依赖注入系统并配置其他模块的主要方法.例:

``` cs
public class StartupModule:ScorpioModule
{
    public override void ConfigureServices(ConfigureServicesContext context)
    {
        context.Services.RegisterAssemblyByConvention();
    }
}
```

你可以按照Microsoft的[文档](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)中的说明逐个注册依赖项.但Scorpio 有一个通用的依赖注册系统,可以自动注册程序集中的所有服务.有关依赖项注入系统的更多信息,请参阅[依赖注入](dependency-injection)文档.

你也可以通过这种方式配置其他服务和模块.例:

``` cs
public class StartupModule:ScorpioModule
{
    public override void ConfigureServices(ConfigureServicesContext context)
    {
        context.Services.PreConfigure<DataFilterOptions>(options =>
        {
            options.Configure<ISoftDelete>(f=>f.Expression(d=>d.IsDeleted==false));
        });
        context.Services.RegisterAssemblyByConvention();
    }
}
```

有关配置系统的更多信息,请参阅配置（TODO:link）文档.

### 配置服务的预处理与后处理

`ScorpioModule`{:.language-cs} 类还定义了`PreConfigureServices`{:.language-cs}和`PostConfigureServices`{:.language-cs}方法用来在`ConfigureServices`{:.language-cs}之前或之后重写这些方法并编写你的代码。请注意,在这些方法中编写的代码将在所有其他模块的`ConfigureServices`{:.language-cs}方法之前或之后执行。

## 应用程序初始化

一旦配置了所有模块的所有服务,应用程序就会通过初始化所有模块来启动。在此阶段，你可以从`IServiceProvider`{:.language-cs}中获取服务，因为这时它已准备就绪且可用。

### Initialize方法

你可以在启动应用程序时重写`Initialize`{:.language-cs}方法来执行代码。例：

``` cs
public class StartupModule:ScorpioModule
{
    public override void Initialize(ApplicationInitializationContext context)
    {
        Console.WriteLine($"Module {nameof(StartupModule)} is initialized.");
    }
}
```

### 应用程序初始化的预处理与后处理

ScorpioModule类还定义了`PreInitialize`{:.language-cs}和`PostInitialize`{:.language-cs}方法用来在`Initialize`{:.language-cs}之前或之后重写这些方法并编写你的代码。请注意,在这些方法中编写的代码将在所有其他模块的`Initialize`{:.language-cs}方法之前或之后执行。

## 应用程序关闭

最后,如果要在应用程序关闭时执行某些代码,你可以覆盖`Shutdown`{:.language-cs}方法。如：

``` cs
public class StartupModule:ScorpioModule
{
    public override void Shutdown(ApplicationShutdownContext context)
    {
        Console.WriteLine($"Module {nameof(ApplicationModule)} is shutdown.");
    }
}
```

## 模块依赖

在模块化应用程序中，一个模块必须依赖于另一个或几个模块。如果一个 Scorpio 模块依赖于另一个模块，请在模块类上声明`[DependsOn]`{:.language-cs}特性,如下所示:

``` cs
[DependsOn(typeof(EntityFrameworkCoreModule))]
[DependsOn(typeof(Application.ApplicationModule))]
public class StartupModule:ScorpioModule
{
    //...
}
```

你可以根据需要使用多个`[DependsOn]`{:.language-cs}特性或将多个模块类型传递给单个`[DependsOn]`{:.language-cs}特性。如果您没有为您的模块声明`[DependsOn]`{:.language-cs}，该模块将默认依赖 `KernelModule`{:.language-cs} 模块。

依赖模块可能依赖于另一个模块,但你只需要定义直接依赖项.Scorpio 在启动时会分析应用程序的依赖关系,并以正确的顺序初始化/关闭模块。