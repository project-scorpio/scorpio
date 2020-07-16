#tool "nuget:?package=xunit.runner.console&version=2.3.1"
#addin "nuget:?package=Cake.Sonar&version=1.1.25"
#tool "nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.8.0"
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
#load "./build/index.cake"
var target = Argument("target", "Default");
var service = new BuildService(Context);

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
	.Does(() =>{
	service.Clean();
});
Task("Build").IsDependentOn("Clean")
	.Does(() =>{
	service.Build();
});
Task("Test")
	.IsDependentOn("Build")
	.Does(() =>{
	service.Test();
});

Task("Package")
    .IsDependentOn("Test")
    .WithCriteria(() =>service.Context.Environment.IsPublish)
    .Does(()=>{
	service.Package();
});

	Task("Publish")
    .IsDependentOn("Package")
    .WithCriteria(() =>service.Context.Environment.IsPublish)
    .Does(()=>{
    service.Publish();
});

Task("Default").IsDependentOn("Build").IsDependentOn("Package").IsDependentOn("Publish");

RunTarget(target);