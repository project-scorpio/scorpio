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
        <img class="img-fluid mb-3 mb-md-0" src="{{site.baseurl}}/assets/img/logo.png" alt="" width="1024" height="860">
      </div>
      <div class="col-md-6 order-md-1 text-center text-md-left pr-md-5">
        <h1 class="mb-3 bd-text-purple-bright">Scorpio</h1>
        <p class="lead">
          Scorpio is a modular-based cross-platform framework for the .NET Core and .NET frameworks, with project include AOP, modularity, plug-ins, dependency injection, and some common extension and modules.
        </p>
        <div class="d-flex flex-column flex-md-row lead mb-3">
          <a href="{{site.baseurl}}/docs/getting-started/" class="btn btn-lg btn-bd-primary mb-3 mb-md-0 mr-md-3" onclick="ga('send', 'event', 'Jumbotron actions', 'Get started', 'Get started');">Getting Started</a>
          <a href="{{site.baseurl}}/download/" class="btn btn-lg btn-outline-secondary" onclick="ga('send', 'event', 'Jumbotron actions', 'Download', 'Download 4.0.0');">Download</a>
        </div>
        <p class="text-muted mb-0">
           Currently Version：v0.1.0-alpha.3 | Document update ：2019-09-12
        </p>
      </div>
    </div>
    
  </div>
</main>
<div class="masthead-followup row m-0 border border-white">
  <div class="col-12 col-md-4 p-3 p-md-5 bg-light border border-white">
    <!-- Icon by Bytesize https://github.com/danklammer/bytesize-icons -->
    {% include default-package-icon.svg %}

    <h3>Install Nuget package</h3>
    <p>Install Scorpio and it’s dependencies using nuget. You will need to create a netstandard2.0 project and bring the package into it. </p>

<figure class="highlight"><pre><code class="language-powershell" data-lang="sh">Install-Package Scropio <span class="nt">-v</span> 0.1.0-alpha.3</code></pre></figure>

<figure class="highlight"><pre><code class="language-sh" data-lang="sh">dotnet <span class="nb">add package </span>Scropio <span class="nt">-v</span> 0.1.0-alpha.3</code></pre></figure>

    <hr class="half-rule">
    <a class="btn btn-outline-primary" href="{{site.baseurl}}/download">Installation Docs</a>
  </div>

  <div class="col-12 col-md-4 p-3 p-md-5 bg-light border border-white">
    <!-- Icon by Bytesize https://github.com/danklammer/bytesize-icons -->
    <svg xmlns="http://www.w3.org/2000/svg" focusable="false" width="32" height="32" fill="none" stroke="currentcolor" stroke-width="2" class="text-primary mb-2" viewBox="0 0 32 32" stroke-linecap="round" stroke-linejoin="round"><title>Download icon</title><path d="M9 22c-9 1-8-10 0-9C6 2 23 2 22 10c10-3 10 13 1 12m-12 4l5 4 5-4m-5-10v14"/></svg>

    <h3>Architecture</h3>
<p>Modern architecture to create maintainable software solutions.</p>
<h5>Modular Design</h5>
<p>Scorpio provides complete modularity system to allow you to develop reusable application modules.</p>

<h5>Microservice Compatible</h5>
<p>The core framework & pre-build modules are designed the microservice architecture in mind.</p>
    <hr class="half-rule">
    <a class="btn btn-outline-primary" href="{{site.baseurl}}/docs">Explore the docs</a>
  </div>

  <div class="col-12 col-md-4 p-3 p-md-5 bg-light border border-white">
    <!-- Icon by Bytesize https://github.com/danklammer/bytesize-icons -->
    <svg xmlns="http://www.w3.org/2000/svg" focusable="false" width="32" height="32" fill="none" stroke="currentcolor" stroke-width="2" class="text-primary mb-2" viewBox="0 0 32 32" stroke-linecap="round" stroke-linejoin="round"><title>Lightning icon</title><path d="M18 13l8-11L8 13l6 6-8 11 18-11z"/></svg>

    <h3>Open source</h3>
    <p>
      We believe in open source and so should you. The source code for Cake is hosted on GitHub and includes everything needed to build it yourself.
    </p>
    <hr class="half-rule">
    <a href="{{site.repo}}" class="btn btn-outline-primary">Browse source</a>
  </div>
</div>