#load "./index.cake"

public class BuildService
{
    private readonly ICakeContext _cakeContext;
    private readonly BuildContext _context;
    private readonly string _nugetRegex;
    private readonly string _nugetApiKey;
    private readonly string _nugetQueryUrl;
    private readonly string _nugetPushUrl;

    private readonly DotNetCoreMSBuildSettings _msbuildSettings;

    internal BuildContext Context => _context;

    public BuildService(ICakeContext context)
    {
        this._cakeContext = context;
        _context=new BuildContext(context);
        _nugetRegex = $"{_context.ArtifactsPath.FullPath}**/*.nupkg";
        _nugetApiKey = _cakeContext.EnvironmentVariable("NugetApiKey");
        _nugetQueryUrl= _cakeContext.EnvironmentVariable("NugetQueryUrl","https://www.myget.org/F/project-scorpio/api/v3/index.json");
        _nugetPushUrl = _cakeContext.EnvironmentVariable("NugetPushUrl");
        _msbuildSettings=new DotNetCoreMSBuildSettings()
										.SetFileVersion(_context.Version.GetFileVersion())
										.SetConfiguration(_context.Environment.Configuration)
										.SetVersion(_context.Version.GetVersion())
										.WithProperty("SourceLinkCreate","true");
    }

    public  void Clean()
    {
        _cakeContext.Information("Begin clean solution");
        _cakeContext.CleanDirectories("./*/src/**/bin");
        _cakeContext.CleanDirectories("./*/test/**/obj");
        _cakeContext.CleanDirectories("./*/src/**/bin");
        _cakeContext.CleanDirectories("./*/test/**/obj");
        _cakeContext.CleanDirectory(_context.ArtifactsPath);
        _cakeContext.Information(_context.Version.GetVersion());
    }

    public void Restore(){
        foreach (var item in _context.ProjectFiles)
        {
            var settings=new DotNetCoreRestoreSettings{
                Sources=new[]{"https://api.nuget.org/v3/index.json",_nugetQueryUrl}

            };
            _cakeContext.DotNetCoreRestore(item.FullPath,new DotNetCoreRestoreSettings{});
        }
    }

    public void Build(){
        var buildSettings=new DotNetCoreBuildSettings{
                Configuration=_context.Environment.Configuration,
                MSBuildSettings=_msbuildSettings
            };
        foreach (var item in _context.ProjectFiles)
        {
            _cakeContext.DotNetCoreBuild(item.FullPath,buildSettings);
        }
    }

    public void Test()
    {
        var testSettings=new DotNetCoreTestSettings{ Configuration=_context.Environment.Configuration};
        foreach (var item in _context.TestProjectFiles)
        {
            _cakeContext.DotNetCoreTest(item.FullPath,testSettings);
        }
    }

    public void Package(){
        var settings=new DotNetCorePackSettings
						{
						   Configuration = _context.Environment.Configuration,
						   OutputDirectory = _context.ArtifactsPath,
						   MSBuildSettings=_msbuildSettings,
						};
        foreach (var item in _context.ProjectFiles)
        {
            _cakeContext.DotNetCorePack(item.FullPath,settings);
        }
    }

    public void Publish(){
        var packages=_cakeContext.GetFiles(_nugetRegex);
        var settings = new DotNetCoreNuGetPushSettings
                         {
						   Source = _nugetPushUrl,
						   ApiKey = _nugetApiKey
                         };
        foreach (var item in packages)
        {
            _cakeContext.DotNetCoreNuGetPush(item.FullPath,settings);
        }
    }
    
}
