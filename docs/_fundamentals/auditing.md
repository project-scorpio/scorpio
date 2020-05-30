---
title: 审计日志
description: Scorpio 审计日志
---

# Scorpio 审计日志

本文介绍了 Scorpio 审计日志 ([`Scorpio.Auditing`{:.language-cs}](https://www.nuget.org/packages/Scorpio.Auditing)) 并提供有关使用方法的指南。


## 简介

Scorpio 框架提供了一个可扩展的审计日志系统,自动化的根据约定记录审计日志,并提供配置控制审计日志的级别.

一个审计日志对象(参见下面的审计日志对象部分)通常是针对每个web请求或事物处理创建和保存的.包括;

+ 请求和响应的细节 (如URL,HTTP方法,浏览器信息,HTTP状态代码...等).
+ 执行的动作 (控制器操作和应用服务方法调用及其参数).
+ 实体的变化 (在Web请求中).
+ 异常信息 (如果在执行请求发生操作).
+ 请求时长 (测量应用程序的性能).

## AuditingOptions

`AuditingOptions`{:.language-cs} 是配置审计日志系统的主要options对象. 你可以在模块的 `ConfigureServices()`{:.language-cs} 方法中进行配置:

``` cs
Configure<AuditingOptions>(options =>
{
    options.IsEnabled = false; //Disables the auditing system
});
```

这里是你可以配置的选项列表:

+ `IsEnabled`{:.language-cs} (默认值: `true`{:.language-cs}): 启用或禁用审计系统的总开关. 如果值为 `false`{:.language-cs},则不使用其他选项.
+ `IsEnabledForAnonymousUsers`{:.language-cs} (默认值: `true`{:.language-cs}): 如果只想为经过身份验证的用户记录审计日志,请设置为 `false`{:.language-cs}.如果为匿名用户保存审计日志,你将看到这些用户的 `CurrentUser`{:.language-cs} 值为 `null`{:.language-cs}.
+ `ApplicationName`{:.language-cs}: 如果有多个应用程序保存审计日志到单一的数据库,使用此属性设置为你的应用程序名称区分不同的应用程序日志.
+ `IgnoredTypes`{:.language-cs}: 审计日志系统忽略的 `Type`{:.language-cs} 列表. 如果它是实体类型,则不会保存此类型实体的更改. 在序列化操作参数时也使用此列表.
+ `Contributors`{:.language-cs}: `IAuditContributor`{:.language-cs} 实现的列表. 贡献者是扩展审计日志系统的一种方式. 有关详细信息请参阅下面的"审计日志贡献者"部分.

## 启用/禁用审计日志服务

您可以为任何类型的类(注册到[依赖注入](dependency-injection)并从依赖注入解析)启用审计日志.
对于任何需要被审计记录的类或方法都可以使用 `[AuditedAttribute]` .
并且您可以对部分需要关闭审计日志的类或方法使用 `[DisableAuditingAttribute]`.

## IAuditScope & IAuditingManager

本节介绍用于高级用例的 IAuditLogScope 和 IAuditingManager 服务.

**审计日志范围**是**构建**和**保存**审计日志对象的环境范围. 

### 获取当前审计日志范围

上面提到,审计日志贡献者是操作审计日志对象的全局方法. 你可从服务中获得值.

如果需要在应用程序的任意位置上操作审计日志对象,可以访问当前审计日志范围并获取当前审计日志对象(与范围的管理方式无关). 例:

``` cs
public class MyService : ITransientDependency
{
    private readonly IAuditingManager _auditingManager;

    public MyService(IAuditingManager auditingManager)
    {
        _auditingManager = auditingManager;
    }

    public async Task DoItAsync()
    {
        var currentAuditLogScope = _auditingManager.Current;
        if (currentAuditLogScope != null)
        {
            currentAuditLogScope.Info.Comments.Add(
                "Executed the MyService.DoItAsync method :)"
            );

            currentAuditLogScope.Info.SetExtraProperty("MyCustomProperty", 42);
        }
    }
}
```

总是检查 `_auditingManager.Current` 是否为空,因为它是在外部范围中控制的,在调用方法之前你不知道是否创建了审计日志范围.

### 手动创建审计日志范围

你如果需要手动创建审计日志的范围,可以使用 `IAuditingManager` 创建审计日志的范围. 例:

``` cs
public class MyService : ITransientDependency
{
    private readonly IAuditingManager _auditingManager;

    public MyService(IAuditingManager auditingManager)
    {
        _auditingManager = auditingManager;
    }

    public async Task DoItAsync()
    {
        using (var auditingScope = _auditingManager.BeginScope())
        {
            try
            {
                //Call other services...
            }
            catch (Exception ex)
            {
                //添加异常。
                _auditingManager.Current.Info.Exceptions.Add(ex);
            }
            finally
            {
                //保存审计日志，该行代码不是必须的。auditingScope在被释放时会自动保存审计日志。
                await auditingScope.SaveAsync();
            }
        }
    }
}
```

你可以调用其他服务,它们可能调用其他服务,它们可能更改实体,等等. 所有这些交互都保存为finally块中的一个审计日志对象.

## 审计日志贡献者

你可以创建类继承 `AuditLogContributor` 类 来扩展审计系统,该类定义了 `PreContribute` 和 `PostContribute` 方法.

贡献者可以设置 `AuditInfo` 类的属性和集合来添加更多信息.

例:

``` cs
public class MyAuditLogContributor : IAuditContributor
{
    public override void PreContribute(AuditContributionContext context)
    {
        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
        context.AuditInfo.SetExtraProperty(
            "MyCustomClaimValue",
            currentUser.FindClaimValue("MyCustomClaim")
        );
    }

    public override void PostContribute(AuditContributionContext context)
    {
        context.AuditInfo.Comments.Add("Some comment...");
    }
}

```

+ `context.ServiceProvider` 可以从[依赖注入](dependency-injection)系统中解析服务.
+ `context.AuditInfo` 可以用来访问当前审计日志的对象并进行操作.

创建贡献者后,需要将其添加到 `AuditingOptions.Contributors` 列表中:

``` cs
Configure<AuditingOptions>(options =>
{
    options.Contributors.Add(new MyAuditLogContributor());
});

```

## IAuditingStore

`IAuditingStore` 是一个接口,用于保存ABP框架的审计日志对象(下面说明). 如果需要将审计日志对象保存到自定义数据存储中,可以在自己的应用程序中实现 `IAuditingStore` 并在[依赖注入](dependency-injection)系统替换.

如果没有注册审计存储,则使用 `SimpleLogAuditingStore`. 它只是将审计对象写入标准日志系统.

## 审计日志对象

+ `AuditInfo`: 具有以下属性:
    + `ApplicationName`: 当你保存不同的应用审计日志到同一个数据库,这个属性用来区分应用程序.
    + `CurrentUser`:当前用户,用户未登录为 `null`.
    + `ImpersonatorUser`:当前用户模仿用户,未模仿用户为 `null`.
    + `ExecutionTime`: 审计日志对象创建的时间.
    + `ExecutionDuration`: 请求的总执行时间,以毫秒为单位. 可以用来观察应用程序的性能.
    + `Exceptions`: 审计日志对象可能包含零个或多个异常. 可以得到失败请求的异常信息.
    + `Comments`:用于将自定义消息添加到审计日志条目的任意字符串值. 审计日志对象可能包含零个或多个注释.
+ `AuditActionInfo`: 一个 审计日志动作通常是web请求期间控制器动作或应用服务方法调用. 一个审计日志可以包含多个动作. 动作对象具有以下属性:
    + `ServiceName`:执行的控制器/服务的名称.
    + `MethodName`:控制器/服务执行的方法的名称.
    + `Parameters`:传递给方法的参数的JSON格文本.
    + `ExecutionTime`: 执行的时间.
    + `ExecutionDuration`: 方法执行时长,以毫秒为单位. 可以用来观察方法的性能.

除了上面说明的标准属性之外,`AuditInfo`, `AuditActionInfo` 对象还实现了 `IHasExtraProperties` 接口,你可以向这些对象添加自定义属性.
