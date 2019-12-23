---
title: Scorpio 基础知识
description: Scorpio 基础知识
---

# Scorpio 引导程序

本文介绍了 Scorpio 引导程序 (Bootstrapper) 并提供有关使用方法的指南。

## 什么是 Scorpio 引导程序？

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
            Host.CreateDefaultBuilder(args).AddScorpio<StartupModule>()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
```

## BootstrapperCreationOptions

