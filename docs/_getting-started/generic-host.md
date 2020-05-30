---
title: 快速开始
description: 快速开始一个应用
---

# 快速开始

本文档将介绍如何从头开始以最小的依赖关系启动 Scorpio。

## 创建一个通用主机应用

本节将演示如何快速的创建一个使用 Scorpio 的通用主机应用。

### 创建新的项目


``` bash
$ mkdir Scorpio.Demo.GenericHost
$ dotnet new console
```

### 添加 Scorpio.Hosting 包引用

``` bash
$ dotnet add package Scropio.Hosting -v {{site.currently}}
```

``` powershell
PM> install-Package Scropio.Hosting -v {{site.currently}}
```

### 创建 Scorpio 模块

``` csharp
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;

namespace Scorpio.Demo.GenericHost
{
    public class StartupModule:ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
        }
    }
}

```

### 初始化应用程序

``` cs
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Scorpio.Demo.GenericHost
{
    class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).AddScorpio<StartupModule>();

}


```

### 创建主机服务

上面的应用程序什么都不做, 让我们创建一个主机服务做一些事情:

``` cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scorpio.Demo.GenericHost
{
    class HostedService : Microsoft.Extensions.Hosting.BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() => Console.WriteLine("Background service is stopping!"));
            Console.WriteLine("Background service is startting!");
            for (var i = 1; i>0; i++)
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                await Task.Delay(1000, stoppingToken);
                Console.WriteLine($"Background service is invoked {i} time(s).");
            }
        }

    }
}
```

修改 StartupModule.cs, 如下所示:

``` cs
using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.DependencyInjection;
namespace Scorpio.Demo.GenericHost
{
    public sealed class ApplicationModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            context.Services.AddHostedService<HostedService>();
        }
    }
}
```

### 源码

从这里获取本教程中创建的示例项目的源代码(代码稍后上传).