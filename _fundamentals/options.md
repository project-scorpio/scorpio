---
title: 选项
description: Scorpio 中的选项模式
---
# Scorpio 中的选项模式

微软引入[选项模式](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options),它是用于配置框架服务使用的设置. 选项模式由[Microsoft.Extensions.Options](https://www.nuget.org/packages/Microsoft.Extensions.Options) NuGet包实现,除了ASP.NET Core应用,它还适用于任何类型的应用程序.

Scorpio 框架遵循选项模式,并定义了用于配置框架和模块的选项类(在相关功能文档中有详细的说明).

由于[微软的文档](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options)详细解释了选项模式,本文中只会介绍Scorpio 增加的一些功能.

## 配置选项

通常配置选项在 `Startup`{:.language-cs} 类的 `ConfigureServices()`{:.language-cs} 方法中. 但由于 Scorpio 框架提供了模块化基础设施,因此你可以在模块的 `ConfigureServices()`{:.language-cs} 方法配置选项. 例:

``` cs
public override void ConfigureServices(ConfigureServicesContext context)
{
    context.Services.Configure<AuditingOptions>(options =>
    {
        options.IsEnabled = false;
    });
}
```

如果你正在开发一个可重用的模块,你可能需要定义一个允许开发人员配置模块的选项类. 这时定义一个如下所示的普通类:

``` cs
public class MyOptions
{
    public int Value1 { get; set; }
    public bool Value2 { get; set; }
}
```

然后开发人员可以像上面 `AuditingOptions`{:.language-cs} 示例一样配置你的选项:

``` cs
public override void ConfigureServices(ConfigureServicesContext context)
{
    context.Services.Configure<MyOptions>(options =>
    {
        options.Value1 = 42;
        options.Value2 = true;
    });
}
```

### 获取选项值

在你需要获得一个选项值时,将 `IOptions<TOption>`{:.language-cs} 服务[注入](Dependency-Injection)到你的类中,使用它的 `.Value`{:.language-cs} 属性得到值. 例:

``` cs
public class MyService : ITransientDependency
{
    private readonly MyOptions _options;

    public MyService(IOptions<MyOptions> options)
    {
        _options = options.Value; //Notice the options.Value usage!
    }

    public void DoIt()
    {
        var v1 = _options.Value1;
        var v2 = _options.Value2;
    }
}
```

阅读[微软文档](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options)了解选择模式的所有细节.

## 预配置

选项模式的限制之一是你只能解析(注入) `IOptions <MyOptions>`{:.language-cs} 并在依赖注入配置完成(即所有模块的 `ConfigureServices()`{:.language-cs} 方法完成)后获取选项值.

如果你正在开发一个模块,可能需要让开发者能够设置一些选项,并在依赖注入注册阶段使用这些选项. 你可能需要根据选项值配置其他服务或更改依赖注入的注册代码.

对于此类情况,请使用 `IPreConfigureOptions<TOptions>`{:.language-cs} 设置预配置。 进行所有 `IConfigureOptions<TOptions>`{:.language-cs} 配置后运行预配置：

你的模块中定义计划选项类. 例:
``` cs
public class MyPreOptions
{
    public bool MyValue { get; set; }
}
```
然后任何依赖于模块的模块类都可以在其 `PreConfigureServices()`{:.language-cs} 方法中使用 `PreConfigure<TOptions>()`{:.language-cs} 方法. 例:
``` cs
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    context.Services.PreConfigure<MyPreOptions>(options =>
    {
        options.MyValue = true;
    });
}
```
`PreConigure<TOptions>()`{:.language-cs} 方法可以对命名选项进行预配置.
``` cs
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    context.Services.PreConfigure<MyPreOptions>("Named_Options_1",options =>
    {
        options.MyValue = true;
    });
}
```

使用 `PreConfigureAll<TOptions>()`{:.language-cs} 方法对所有选项实例进行预配置.
``` cs
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    context.Services.PreConfigureAll<MyPreOptions>(options =>
    {
        options.MyValue = true;
    });
}
```


>多个模块可以预配置选项,并根据它们的依赖顺序覆盖选项值.