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


## IAuditScope & IAuditingManager

