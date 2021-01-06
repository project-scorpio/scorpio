---
title: Scorpio 引导程序
description: Scorpio 引导程序
---

# Scorpio 引导程序

本文介绍了 Scorpio 引导程序 (`Bootstrapper`{:.language-cs}) 并提供有关使用方法的指南。

## 什么是 Scorpio 引导程序？

`Bootstrapper`{:.language-cs} 类主要用于启动应用程序，发现并装载 `ScorpioModule`{:.language-cs} 类及其依赖链，完成应用启动前的初始化操作，以及应用程序的生命周期管理。

`Bootstrapper`{:.language-cs} 类由 `BootStrapper.Create`{:.language-cs} 方法创建或依赖于 Asp.Net Core 的通用主机构造器。如：

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
            Host.CreateDefaultBuilder(args).AddScorpio<StartupModule>()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
```

## 引导程序创建选项

您可以使用 `Bootstrapper.Create<StartupModule>(Action<BootstrapperCreationOptions> optionsAction)`{:.language-cs} 或 `IHostBuilder.AddScorpio<StartupModule>(Action<BootstrapperCreationOptions> optionsAction)`{:.language-cs} 重载以便在创建 Bootstrapper 的时候提供更多的选项。`BootstrapperCreationOptions`{:.language-cs} 提供的服务如下：

+ `PreConfigureServices(Action<ConfigureServicesContext> configureDelegate)`{:.language-cs}
  
  用于在Module之外提供配置服务的预处理操作。

+ `ConfigureServices(Action<ConfigureServicesContext> configureDelegate)`{:.language-cs}
  
  用于在Module之外提供配置服务操作。
  
+ `PostConfigureServices(Action<ConfigureServicesContext> configureDelegate)`{:.language-cs}
  
  用于在Module之外提供配置服务的后处理操作。

+ `Configuration(Action<IConfigurationBuilder> action)`{:.language-cs}
  
  用于为应用程序添加应用配置。

以上操作可以多次添加，引导程序将在处理所有 Module 模块的对应方法之后依照添加的顺序依次执行。

+ `UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)`{:.language-cs}
  
  用于替换应用的默认容器提供程序。

