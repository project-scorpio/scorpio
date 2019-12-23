---
title: Scorpio 基础知识
description: Scorpio 基础知识
---

# Scorpio 基础知识

本文概述了了解如何开发 Scorpio 应用的关键主题。

## Bootstrapper 

Bootstrapper 类主要用于启动应用程序，发现并装载 ScorpioModule 类及其依赖链，完成应用启动前的初始化操作，以及应用程序的生命周期管理。

Bootstrapper 类由 BootStrapper.Create 方法创建或依赖于 Asp.Net Core 的通用主机构造器。如：

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

Modularity 主要由 ScorpioModule 和 DependsOnAttribute 组成。

ScorpioModule 类用于以下目的：

+ 配置应用所需服务
+ 初始化应用
+ 关闭应用时清理应用

服务是应用使用的组件，例如，日志记录组件就是一项服务。请将配置（或注册）服务的代码添加到 ScorpioModule.ConfigureServices 方法中。

您可以在应用启动之前进行某些初始化操作，例如：创建应用请求处理管道等。请将初始化应用的代码添加到 ScorpioModule.Initialize 方法中。

您可以在应用关闭之前进行某些清理操作，例如：关闭应用请求处理管道等。请将关闭应用的代码添加到 ScorpioModule.Shutdown 方法中。

ScorpioModule 使用 DependsOnAttribute 标注 Module 之间的依赖关系。

下面是 ScorpioModule 类示例：

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

Conventional 用于对应用 Assembly 中的 Type 执行一些一般性操作，如：依赖关系注入、Aspect 注入、Unit Of Work 注入等。

Conventional 主要由以下类、接口及方法组成：

+ ConventionalAction 类，用于对执行一般性操作。
+ ConventionalRegistrar 类，用于注册及配置 ConventionalAction 类。
+ ServiceCollection.AddConventionalRegistrar() 方法，用于将 ConventionalRegistrar 添加到 ConventionalRegistrar 列表中。
+ ServiceCollection.RegisterAssemblyByConvention() 方法，用于对指定的 Assembly 执行 ConventionalAction 操作。

在 Scorpio 中，各种自动化操作便是基于 Conventional 设计并实现的。如果您需要实现自己的一般性操作。请实现 ConventionalActionBase 抽象类及 IConventionalRegistrar 接口。

## Dependency Injection

Scorpio 的 DependencyInjection 直接依赖于 Asp.Net Core 的 DependencyInjection 组件，主要实现了对服务的自动依赖注入。

您可以通过使用以下接口或 Attribue 实现服务的自动依赖注入：

+ ISingletonDependency ，实现 ISingletonDependency 的组件将被注册为 Singleton 生命周期的服务。
+ ITransientDependency ，实现 ITransientDependency 的组件将被注册为 Transient 生命周期的服务。
+ IScopedDependency ，实现 IScopedDependency 的组件将被注册为 Scoped 生命周期的服务。
+ ExposeServicesAttribute ,通过添加 ExposeServicesAttribute 特性可以为组件实现更加灵活的依赖注入选项。

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

因 Scorpio 的 DependencyInjection 是基于 Conventional 实现的。所以，您可以通过实现自己的 IConventionalRegistrar 实现更多的自动依赖注入方式。

## Options

Scorpio 扩展了 Asp.Net Core 的 Options ，主要是为 Options 的配置添加了 PreConfigure 操作。

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

Scorpio 依赖 AspectCore 框架提供AOP能力，并为Aspect提供自动注册的能力。在默认的情况下，Aspect 处于关闭状态，如果您需要打开Aspect ，请在构建 Bootstrapper 时使用 BootstrapperCreationOptions.UseAspectCore ,在打开 Aspect 后，Scorpio 的 DependencyInjection 将由 AspcectCore 提供。

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
