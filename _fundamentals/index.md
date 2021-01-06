---
title: Scorpio 基础知识
description: Scorpio 基础知识
---

# Scorpio 基础知识

本文概述了了解如何开发 Scorpio 应用的关键主题。

## Bootstrapper 

`Bootstrapper`{:.language-cs} 类主要用于启动应用程序，发现并装载 `ScorpioModule`{:.language-cs} 类及其依赖链，完成应用启动前的初始化操作，以及应用程序的生命周期管理。

`Bootstrapper`{:.language-cs} 类由 `BootStrapper.Create()`{:.language-cs} 方法创建或依赖于 Asp.Net Core 的通用主机构造器。如：

``` cs

using System;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Sample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bootstrapper = Bootstrapper.Create<StartupModule>())
            {
                bootstrapper.Initialize();
            }
        }
    }
}
```
或者：

``` cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Scorpio.Sample.AspnetCore
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).AddBootstrapper<StartupModule>()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
```

## Modularity 

`Modularity`{:.language-cs} 主要由 `ScorpioModule`{:.language-cs} 和 `DependsOnAttribute`{:.language-cs} 组成。

`ScorpioModule`{:.language-cs} 类用于以下目的：

+ 配置应用所需服务
+ 初始化应用
+ 关闭应用时清理应用

服务是应用使用的组件，例如，日志记录组件就是一项服务。请将配置（或注册）服务的代码添加到 `ScorpioModule.ConfigureServices()`{:.language-cs} 方法中。

您可以在应用启动之前进行某些初始化操作，例如：创建应用请求处理管道等。请将初始化应用的代码添加到 `ScorpioModule.Initialize()`{:.language-cs} 方法中。

您可以在应用关闭之前进行某些清理操作，例如：关闭应用请求处理管道等。请将关闭应用的代码添加到 `ScorpioModule.Shutdown()`{:.language-cs} 方法中。

`ScorpioModule`{:.language-cs} 使用 `DependsOnAttribute`{:.language-cs} 标注 Module 之间的依赖关系。

下面是 `ScorpioModule`{:.language-cs} 类示例：

```cs
using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Sample.Application;
namespace Scorpio.Sample.AspnetCore
{
    [DependsOn(typeof(ApplicationModule))]
    public class StartupModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
        }

        public override void Initialize(ApplicationInitializationContext context)
        {
            Console.WriteLine($"Module {nameof(SampleModule)} is initialized.");
        }

        public override void Shutdown(ApplicationShutdownContext context)
        {
            Console.WriteLine($"Module {nameof(SampleModule)} is shutdown.");
        }
    }
}
```
有关详细信息，请参阅 Scorpio 中的应用启动。

## Conventional

`Conventional`{:.language-cs} 用于对应用 `Assembly`{:.language-cs} 中的 `Type`{:.language-cs} 执行一些常规操作，如：依赖关系注入、Aspect 注入、Unit Of Work 注入等。

`Conventional`{:.language-cs} 主要由以下类、接口及方法组成：

+ `ConventionalAction`{:.language-cs} 类，用于对执行一般性操作。
+ `ConventionalRegistrar`{:.language-cs} 类，用于注册及配置 `ConventionalAction`{:.language-cs} 类。
+ `ServiceCollection.AddConventionalRegistrar()`{:.language-cs} 方法，用于将 `ConventionalRegistrar`{:.language-cs} 添加到 `ConventionalRegistrar`{:.language-cs} 列表中。
+ `ServiceCollection.RegisterAssemblyByConvention()`{:.language-cs} 方法，用于对指定的 `Assembly`{:.language-cs} 执行 `ConventionalAction`{:.language-cs} 操作。

在 Scorpio 中，各种自动化操作便是基于 `Conventional`{:.language-cs} 设计并实现的。如果您需要实现自己的一般性操作。请实现 `ConventionalActionBase`{:.language-cs} 抽象类及 `IConventionalRegistrar`{:.language-cs} 接口。

## Dependency Injection

Scorpio 的 `DependencyInjection`{:.language-cs} 直接依赖于 Asp.Net Core 的 `DependencyInjection`{:.language-cs} 组件，主要实现了对服务的自动依赖注入。

您可以通过使用以下接口或 `Attribue`{:.language-cs} 实现服务的自动依赖注入：

+ `ISingletonDependency`{:.language-cs} ，实现 `ISingletonDependency`{:.language-cs} 的组件将被注册为 Singleton 生命周期的服务。
+ `ITransientDependency`{:.language-cs} ，实现 `ITransientDependency`{:.language-cs} 的组件将被注册为 Transient 生命周期的服务。
+ `IScopedDependency`{:.language-cs} ，实现 `IScopedDependency`{:.language-cs} 的组件将被注册为 Scoped 生命周期的服务。
+ `ExposeServicesAttribute`{:.language-cs} ,通过添加 `ExposeServicesAttribute`{:.language-cs} 特性可以为组件实现更加灵活的依赖注入选项。

如：

``` cs
class SingletonService:ISingletonService,ISingletonDependency
{

}

[ExposeServices(typeof(IExposedService),ServiceLifetime=ServiceLifetime.Transient)]
class ExposedService:IExposedService
{

}
```

因 Scorpio 的 `DependencyInjection`{:.language-cs} 是基于 `Conventional`{:.language-cs} 实现的。所以，您可以通过实现自己的 `IConventionalRegistrar`{:.language-cs} 实现更多的自动依赖注入方式。

## Options

Scorpio 扩展了 Asp.Net Core 的 `Options`{:.language-cs} ，主要是为 `Options`{:.language-cs} 的配置添加了 `PreConfigure`{:.language-cs} 操作。

如：

``` cs
public override void ConfigureServices(ConfigureServicesContext context)
{
    context.Services.PreConfigure<DataFilterOptions>(options =>
    {
        options.Configure<ISoftDelete>(f=>f.Expression(d=>d.IsDeleted==false));
    });
    context.Services.AddSingleton(typeof(IDataFilter<>), typeof(DataFilter<>));
    context.Services.RegisterAssemblyByConvention();
}
```

## Aspect

Scorpio 依赖 `AspectCore`{:.language-cs} 框架提供AOP能力，并为Aspect提供自动注册的能力。在默认的情况下，Aspect 处于关闭状态，如果您需要打开Aspect ，请在构建 `Bootstrapper`{:.language-cs} 时使用 `BootstrapperCreationOptions.UseAspectCore()`{:.language-cs} ,在打开 Aspect 后，Scorpio 的 `DependencyInjection`{:.language-cs} 将由 `AspcectCore`{:.language-cs} 提供。

如：

``` cs
using System;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Sample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bootstrapper = Bootstrapper.Create<StartupModule>(opt=>opt.UseAspectCore()))
            {
                bootstrapper.Initialize();
            }
        }
    }
}
```
