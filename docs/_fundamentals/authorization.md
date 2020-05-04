---
title: 授权
description: Scorpio 授权
---

# Scorpio 授权

本文介绍了 Scorpio 审计 ([`Scorpio.Authorization`{:.language-cs}](https://www.nuget.org/packages/Scorpio.Authorization)) 并提供有关使用方法的指南。

## 简介

授权用于检查是否允许用户在应用程序中执行某些特定操作。

Scorpio 为您提供了基于权限模型的灵活的授权系统，您可以很简单的将 Scorpio 的授权系统和ASP.NET Core 的授权系统集成，并在您的应用服务中使用授权系统，为您的应用服务提供可达到代码级别的授权管理。

## 授权管理

在 Scorpio 您可以使用 `IAuthorizationManager`{:.language-cs} 控制授权，也可以在打开 Aspect 后，使用 `AuthorizeAttribute`{:.language-cs} 特性控制授权。

您可以使用 `IAuthorizationManager`{:.language-cs} 在您的代码中有条件的控制授权。如：

``` csharp
public async Task CreateAsync(CreateAuthorDto input)
{
    await _authorizationManager.AuthorizeAsync(false,"Author_Management_Create_Books");
    
    //continue to the normal flow...
}
```

## 权限管理

权限系统是为特定用户,角色或客户端授权或禁止的简单策略。Scorpio 为您提供了完整的权限模型，您可以很简单的定义您的权限模型。

### 定义权限

您可以通过创建一个实现了 `IPermissionDefinitionProvider`{:.language-cs} 接口的类来注入您的权限定义。

``` cs
using Scorpio.Authorization.Permissions;

namespace Scorpio.Sample.Authroization.Permissions
{
    public class SamplePermissionDefinitionProvider : IPermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup("SampleGroup");

            myGroup.AddPermission("Author_Management_Create_Books");
        }
    }
}
```
你需要在 `Define`{:.language-cs} 方法中添加权限组或者获取已存在的权限组,并向权限组中添加权限。

>Scorpio 会自动发现并注入这个类,您需要在 `PermissionOptions.DefinitionProviders`{:.language-cs} 中添加您的提供者 !

``` cs
context.Services.Configure<PermissionOptions>(opt=>
            {
                opt.DefinitionProviders.Add<SamplePermissionDefinitionProvider>();
            });
```

### 权限授予提供者

Scorpio 并没有内置权限授予逻辑，您需要自己通过实现 `IPermissionGrantingProvider`{:.language-cs} 来完成权限的授予。如：

``` cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Authorization.Permissions
{
    class RoleBasePermissionGrantingProvider : IPermissionGrantingProvider
    {
        public string Name { get; } = "RoleBaseProvider";

        public Task<PermissionGrantingInfo> GrantAsync(PermissionGrantingContext context)
        {
            var success = context.Principal.IsInRole(context.Permission.Name);
            return Task.FromResult(new PermissionGrantingInfo(success,Name));
        }
    }
}
```

>Scorpio 会自动发现并注入这个类,您需要在 `PermissionOptions.GrantingProviders`{:.language-cs} 中添加您的提供者 !

``` cs
context.Services.Configure<PermissionOptions>(opt=>
            {
                opt.GrantingProviders.Add<RoleBasePermissionGrantingProvider>();
            });
```