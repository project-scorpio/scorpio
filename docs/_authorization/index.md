---
title: 授权
description: Scorpio 授权
---

# Scorpio 授权

## 简介

授权用于检查是否允许用户在应用程序中执行某些特定操作。

Scorpio 为您提供了基于权限模型的灵活的授权系统，您可以很简单的将 Scorpio 的授权系统和ASP.NET Core 的授权系统集成，并在您的应用服务中使用授权系统，为您的应用服务提供可达到代码级别的授权管理。

## 授权管理

在 Scorpio 您可以使用 `IAuthorizationManager` 控制授权，也可以在打开 Aspect 后，使用 `AuthorizeAttribute` 特性控制授权。

您可以使用 `IAuthorizationManager` 在您的代码中有条件的控制授权。如：

``` csharp
public async Task CreateAsync(CreateAuthorDto input)
{
    await _authorizationManager.AuthorizeAsync(false,"Author_Management_Create_Books");
    
    //continue to the normal flow...
}
```

## 权限管理

Scorpio 为您提供了完整的权限模型，

