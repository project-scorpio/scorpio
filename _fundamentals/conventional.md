---
title: Scorpio 常规性操作
description: Scorpio 常规性操作
---

# Scorpio 常规性操作

## 什么是 Scorpio 常规性操作

`Conventional`{:.language-cs} 用于对应用 `Assembly`{:.language-cs} 中的 `Type`{:.language-cs} 执行一些常规操作。如：自动的依赖关系注入、Aspect 注入、Unit Of Work 注入等。

`Conventional`{:.language-cs} 主要由以下类、接口及方法组成：

+ `ConventionalAction`{:.language-cs} 类，用于对执行一般性操作。
+ `ConventionalRegistrar`{:.language-cs} 类，用于注册及配置 `ConventionalAction`{:.language-cs} 类。
+ `ServiceCollection.AddConventionalRegistrar()`{:.language-cs} 方法，用于将 `ConventionalRegistrar`{:.language-cs} 添加到 `ConventionalRegistrar`{:.language-cs} 列表中。
+ `ServiceCollection.RegisterAssemblyByConvention()`{:.language-cs} 方法，用于对指定的 `Assembly`{:.language-cs} 执行 `ConventionalAction`{:.language-cs} 操作。

在 Scorpio 中，各种自动化操作便是基于 Conventional 设计并实现的。如果您需要实现自己的一般性操作。请实现 `ConventionalActionBase`{:.language-cs} 抽象类及 `IConventionalRegistrar`{:.language-cs} 接口。以下为 Scorpio 的 Dependency Injection 组件的实现代码：

``` cs
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using System.Linq.Expressions;

namespace Scorpio.DependencyInjection.Conventional
{
    internal class BasicConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.DoConventionalAction<ConventionalDependencyAction>(config =>
            {
                config
                    .Where(t => t.IsStandardType())
                    .Where(t => t.IsAssignableTo<ISingletonDependency>())
                    .AsDefault().AsSelf().Lifetime(ServiceLifetime.Singleton);
                config
                    .Where(t => t.IsStandardType())
                    .Where(t => t.IsAssignableTo<ITransientDependency>())
                    .AsDefault().AsSelf().Lifetime(ServiceLifetime.Transient);
                config
                    .Where(t => t.IsStandardType())
                    .Where(t => t.IsAssignableTo<IScopedDependency>())
                    .AsDefault().AsSelf().Lifetime(ServiceLifetime.Scoped);
                config
                    .Where(t => t.IsStandardType())
                    .Where(t => t.AttributeExists<ExposeServicesAttribute>(false))
                    .AsExposeService();
            });
        }
    }
}

```

``` cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scorpio.Conventional;

namespace Scorpio.DependencyInjection.Conventional
{
    class ConventionalDependencyAction : ConventionalActionBase
    {

        
        public ConventionalDependencyAction(IConventionalConfiguration configuration) 
            : base(configuration)
        {
        }

        protected override void Action(IConventionalContext context)
        {

            context.Types.ForEach(
                t => context.Get<ICollection<IRegisterAssemblyServiceSelector>>("Service").ForEach(
                    selector => selector.Select(t).ForEach(
                    s => context.Services.ReplaceOrAdd(
                        ServiceDescriptor.Describe(s, t, 
                        context.GetOrAdd<IRegisterAssemblyLifetimeSelector>("Lifetime", 
                        new LifetimeSelector(ServiceLifetime.Transient)).Select(t)),
                        t.GetAttribute<ReplaceServiceAttribute>()?.ReplaceService??false
                        ))));
        }
    }
}
```

## ConventionalActionBase 类

您如果需要对类执行一些常规的操作，您首先需要继承 `ConventionalActionBase`{:.language-cs} 类并重写这个类的 `Action(IConventionalContext context)`{:.language-cs} 方法。如：

``` cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scorpio.Conventional;

namespace Scorpio.DependencyInjection.Conventional
{
    class ConventionalDependencyAction : ConventionalActionBase
    {

        
        public ConventionalDependencyAction(IConventionalConfiguration configuration) 
            : base(configuration)
        {
        }

        protected override void Action(IConventionalContext context)
        {

            context.Types.ForEach(t=> {
                //TODO: Do something for the type.
            })
        }
    }
}
```

## IConventionalRegistrar

在编写完成常规操作代码后，我们需要实现 `IConventionalRegistrar`{:.language-cs} 接口以便将编写好的操作代码注册并配置到 `Conventional`{:.language-cs} 中，如：

``` cs
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using System.Linq.Expressions;

namespace Scorpio.DependencyInjection.Conventional
{
    internal class BasicConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.DoConventionalAction<ConventionalDependencyAction>(config =>
            {
                config.CreateContext().Where(t=>/*添加过滤条件*/);
                //或者
                config.Where(t=>/*添加过滤条件*/);//这是个快捷方法，和上面的语句等效。
            });
        }
    }
}
```

最后，请调用 `ServiceCollection.AddConventionalRegistrar()`{:.language-cs} 方法将我们的注册器添加到 Scorpio 中。注册器将在调用 `ServiceCollection.RegisterAssemblyByConvention()`{:.language-cs} 或者它的重载方法时被依次调用。 如：

``` cs
using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Scorpio.Threading;
using Scorpio.Runtime;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class StartupModule : ScorpioModule
    {

        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar(new BasicConventionalRegistrar());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
        }

    }
}
```
> 请注意：
> 
> 请在 `PreConfigureServices()`{:.language-cs} 方法中调用 `AddConventionalRegistrar()`{:.language-cs} 方法，而在 `ConfigureServices()`{:.language-cs} 方法中调用 `RegisterAssemblyByConvention()`{:.language-cs} 方法，以确保在调用任何 `RegisterAssemblyByConvention()`{:.language-cs} 之前所有的注册器已经就绪。