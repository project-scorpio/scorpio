---
layout: default
title: Scorpio project
permalink: /
category: Index Page
---
   <main class="bd-masthead" id="content" role="main">
  <div class="container">
    <div class="row align-items-center">
      <div class="col-6 mx-auto col-md-6 order-md-2">
        {% include logo-boot.svg %}
      </div>
      <div class="col-md-6 order-md-1 text-center text-md-left pr-md-5">
        <h1 class="mb-3 bd-text-purple-bright">Scorpio</h1>
        <p class="lead">
          Scorpio是用于.NET Core 3.0 + 的基于模块化的跨平台框架，包括AOP，模块化，插件化，依赖项注入以及一些常见的扩展和模块。
        </p>
        <div class="d-flex flex-column flex-md-row lead mb-3">
          <a href="{{site.baseurl}}/docs/getting-started/console" class="btn btn-lg btn-bd-primary mb-3 mb-md-0 mr-md-3" onclick="ga('send', 'event', 'Jumbotron actions', 'Get started', 'Get started');">快速入门</a>
          <a href="{{site.baseurl}}/installation" class="btn btn-lg btn-outline-secondary">下载列表</a>
        </div>
        <p class="text-muted mb-0">
           当前版本：v{{site.currently}} | 文档更新日前 ：{{site.lastupdate}}
        </p>
      </div>
    </div>
    
  </div>
</main>
<div class="masthead-followup row m-0 border border-white">
  <div class="col-12 col-md-4 p-3 p-md-5 bg-light border border-white">
    <!-- Icon by Bytesize https://github.com/danklammer/bytesize-icons -->
    {% include default-package-icon.svg %}

    <h3>安装 Nuget 包</h3>
    <p>使用nuget安装Scorpio及其依赖项。 您需要创建一个NetStandard2.1、.NET Core 3.0+ 的项目。 </p>

<figure class="highlight"><pre><code class="language-powershell" data-lang="sh">Install-Package Scropio <span class="nt">-v</span> {{site.currently}}</code></pre></figure>

<figure class="highlight"><pre><code class="language-sh" data-lang="sh">dotnet <span class="nb">add package </span>Scropio <span class="nt">-v</span> {{site.currently}}</code></pre></figure>

    <hr class="half-rule">
    <a class="btn btn-outline-primary" href="{{site.baseurl}}/installation">下载列表</a>
  </div>

  <div class="col-12 col-md-4 p-3 p-md-5 bg-light border border-white">
    <!-- Icon by Bytesize https://github.com/danklammer/bytesize-icons -->
    <svg xmlns="http://www.w3.org/2000/svg" focusable="false" width="32" height="32" fill="none" stroke="currentcolor" stroke-width="2" class="text-primary mb-2" viewBox="0 0 32 32" stroke-linecap="round" stroke-linejoin="round"><title>Download icon</title><path d="M9 22c-9 1-8-10 0-9C6 2 23 2 22 10c10-3 10 13 1 12m-12 4l5 4 5-4m-5-10v14"/></svg>

    <h3>架构</h3>
<p>用于创建可维护的软件解决方案的现代架构。</p>
<h5>模块化设计</h5>
<p>Scorpio提供完整的模块化系统，可让您开发可重复使用的应用程序模块。</p>

<h5>兼容微服务</h5>
<p>核心框架和预构建模块在设计时就考虑了微服务架构。</p>
    <hr class="half-rule">
    <a class="btn btn-outline-primary" href="{{site.baseurl}}/docs">浏览文档</a>
  </div>

  <div class="col-12 col-md-4 p-3 p-md-5 bg-light border border-white">
    <!-- Icon by Bytesize https://github.com/danklammer/bytesize-icons -->
    <svg xmlns="http://www.w3.org/2000/svg" focusable="false" width="32" height="32" fill="none" stroke="currentcolor" stroke-width="2" class="text-primary mb-2" viewBox="0 0 32 32" stroke-linecap="round" stroke-linejoin="round"><title>Lightning icon</title><path d="M18 13l8-11L8 13l6 6-8 11 18-11z"/></svg>


    <h3>开放源码</h3>
    <p>
      我们相信开源，您也应该相信。 Scorpio 的源代码托管在 GitHub 上，包括了您自己构建它时所需的一切。
    </p>
    <hr class="half-rule">
    <a href="{{site.repo}}" class="btn btn-outline-primary">浏览源码</a>
  </div>
</div>