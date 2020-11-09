#tool "nuget:?package=xunit.runner.console&version=2.3.1"
#addin "nuget:?package=Cake.Sonar&version=1.1.25"
#tool "nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.8.0"
#addin nuget:?package=Cake.Coverlet&version=2.4.2
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
    .WithCriteria(() =>service.Context.Environment.IsLocalBuild)
	.Does(() =>{
	service.Build();
});
Task("Test")
    .WithCriteria(() =>service.Context.Environment.IsLocalBuild)
	.IsDependentOn("Build")
	.Does(() =>{
	service.Test();
	CleanDirectories("./.coverreports/");
	ReportGenerator(
		"./src/**/coverage.opencover.xml",
		"./.coverreports",
		new ReportGeneratorSettings{
			ReportTypes=new List<ReportGeneratorReportType>{ReportGeneratorReportType.Html},
			AssemblyFilters=new List<string>{"-*Test*"}
		}
		);
	StartProcess("cmd","/c start ./.coverreports/index.html");
});

Task("Sonar")
    .WithCriteria(() =>service.Context.Environment.IsRunningOnAppVeyor)
	.IsDependentOn("Clean")
	.Does(() =>{
	service.Sonar();
});

Task("Package")
    .IsDependentOn("Sonar")
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

Task("Default").IsDependentOn("Test").IsDependentOn("Sonar").IsDependentOn("Package").IsDependentOn("Publish");

RunTarget(target);