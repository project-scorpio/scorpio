---
title: 快速开始
description: 快速开始一个应用
---

# 快速开始

本文档将介绍如何从头开始以最小的依赖关系启动 Scorpio。

## 创建一个简单的控制台应用

本节将演示如何快速的创建一个使用 Scorpio 的控制台应用。

### 创建新的项目

您可以使用命令行或者Visual Studio 2019 创建一个新的项目。

#### 用命令行创建项目

``` bash
$ mkdir Scorpio.Demo.Console
$ dotnet new console
```

#### 用Visual Studio 2019 创建项目

+ 第一步：
  ![创建项目]({{site.baseurl}}/assets/img/getting-started/first-console-step1.png){: .img-thumbnail }
+ 第二步：
  ![创建项目]({{site.baseurl}}/assets/img/getting-started/first-console-step2.png){: .img-thumbnail }
+ 第三步：
  ![创建项目]({{site.baseurl}}/assets/img/getting-started/first-console-step3.png){: .img-thumbnail }
+ 第四步：
  ![创建项目]({{site.baseurl}}/assets/img/getting-started/first-console-step4.png){: .img-thumbnail }

### 添加 Scorpio 包引用

``` bash
$ dotnet add package Scropio -v {{site.currently}}
```

``` powershell
PM> install-Package Scropio -v {{site.currently}}
```

### 创建第一个 Scorpio 模块

Scorpio 是一个模块化框架, 它需要一个从 ScorpioModule 类派生的 启动(根)模块 类:

``` csharp
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;

namespace Scorpio.Demo.Console
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

我喜欢使用 StartupModule 来作为启动模块的名称，就如同 Asp.Net Core 中的 Startup 启动类一样。

### 初始化应用程序

``` cs
using System;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Demo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bootstrapper = Bootstrapper.Create<StartupModule>())
            {
                bootstrapper.Initialize();
                Console.WriteLine("Press ENTER to stop application...");
                Console.ReadLine();
            }

        }
    }
}

```
Bootstrapper.Create() 用于创建 Bootstrapper 并加载 StartupModule 作为启动模块。 Initialize() 方法初始化应用程序。

### Hello World!

上面的应用程序什么都不做, 让我们创建一个服务做一些事情:

``` cs
using Scorpio.Auditing;
using Scorpio.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Demo.Console
{
    public interface ISayHelloService
    {
        void SayHello();
    }

    class SayHelloService : ISayHelloService,DependencyInjection.ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
```

ITransientDependency是 Scorpio 的一个特殊接口, 它自动将服务注册为Transient。

现在,我们可以解析 ISayHelloService 并调用SayHello. 更改Program.cs, 如下所示:

``` cs
using System;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bootstrapper = Bootstrapper.Create<StartupModule>())
            {
                bootstrapper.Initialize();
                var service = bootstrapper.ServiceProvider.GetService<ISayHelloService>();
                service.SayHello();
            }
        }
    }
}
```



### 源码

从这里获取本教程中创建的示例项目的源代码(代码稍后上传).