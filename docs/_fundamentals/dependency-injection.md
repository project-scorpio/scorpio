---
title: 依赖注入
description: 依赖注入
---

# 依赖注入

Scorpio 的依赖注入系统是基于Microsoft的[依赖注入扩展](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)库（Microsoft.Extensions.DependencyInjection nuget包）开发的.因此,它的文档在 Scorpio 中也是有效的。

>Scorpio 框架没有对任何第三方DI提供程序的核心依赖, 但您可以使用一个提供程序来支持动态代理(dynamic proxying)和一些高级特性以便 Scorpio 的部分模块能正常工作。我们已经为您提供了 AspectCore 的实现， 更多信息请参阅 [Aspect](aspect) 文档。

