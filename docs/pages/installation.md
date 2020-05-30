---
title: 下载列表
permalink: /installation
---

# 下载列表

Scorpio （当前版本 v{{site.currently}}）提供以下 Nuget 包，请根据需要下载。

## Nuget 安装脚本

您可以通过 Nuget 服务器快速安装 Scorpio 到您的项目中。

``` bash
$ dotnet add package Scropio -v {{site.currently}}
```

``` powershell
PM> install-Package Scropio -v {{site.currently}}
```

{% for group in site.data.packages| group_by:"graduation_year" %}
## {{group.title}}

{% for sub in group.groups %}
### {{sub.title}}

<table>
  <thead>
    <tr>
      <th>包名</th>
      <th>最新发行版</th>
      <th>最新预发行版</th>
      <th>每日构建版</th>
      <th>下载数</th>
    </tr>
  </thead>
  <tbody>
{% for item in sub.items %}
<tr>
      <td>{{item}}</td>
      <td><a href="https://www.nuget.org/packages/{{item}}"><img src="https://img.shields.io/nuget/v/{{item}}" alt="Nuget" /></a></td>
      <td><a href="https://www.nuget.org/packages/{{item}}"><img src="https://img.shields.io/nuget/vpre/{{item}}" alt="Nuget (with prereleases)" /></a></td>
      <td><a href="https://www.myget.org/feed/project-scorpio/package/nuget/{{item}}"><img src="https://img.shields.io/myget/project-scorpio/vpre/{{item}}" alt="MyGet (with prereleases)" /></a></td>
      <td><a href="https://www.nuget.org/packages/{{item}}"><img src="https://img.shields.io/nuget/dt/{{item}}" alt="Nuget" /></a></td>
    </tr>
    {% endfor %}
  </tbody>
</table>
{% endfor %}
{% if group.items != null %}
<table>
  <thead>
    <tr>
      <th>包名</th>
      <th>最新发行版</th>
      <th>最新预发行版</th>
      <th>每日构建版</th>
      <th>下载数</th>
    </tr>
  </thead>
  <tbody>
{% for item in group.items %}
<tr>
      <td>{{item}}</td>
      <td><a href="https://www.nuget.org/packages/{{item}}"><img src="https://img.shields.io/nuget/v/{{item}}" alt="Nuget" /></a></td>
      <td><a href="https://www.nuget.org/packages/{{item}}"><img src="https://img.shields.io/nuget/vpre/{{item}}" alt="Nuget (with prereleases)" /></a></td>
      <td><a href="https://www.myget.org/feed/project-scorpio/package/nuget/{{item}}"><img src="https://img.shields.io/myget/project-scorpio/vpre/{{item}}" alt="MyGet (with prereleases)" /></a></td>
      <td><a href="https://www.nuget.org/packages/{{item}}"><img src="https://img.shields.io/nuget/dt/{{item}}" alt="Nuget" /></a></td>
    </tr>
    {% endfor %}
  </tbody>
</table>
{% endif %}
{% endfor %}