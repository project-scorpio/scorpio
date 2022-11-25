#load "./index.cake"
#addin "nuget:?package=Cake.Sonar&version=1.1.31"
#addin nuget:?package=Cake.Coverlet&version=3.0.2
public class BuildService
{
    private readonly ICakeContext _cakeContext;
    private readonly BuildContext _context;
    private readonly string _nugetRegex;
    private readonly string _nugetApiKey;
    private readonly string _nugetQueryUrl;
    private readonly string _nugetPushUrl;

    private readonly string _symbolsPushUrl;
    private readonly DotNetMSBuildSettings _msbuildSettings;

    internal BuildContext Context => _context;

    public BuildService(ICakeContext context)
    {
        this._cakeContext = context;
        _context=new BuildContext(context);
        _nugetRegex = $"{_context.ArtifactsPath.FullPath}**/*.nupkg";
        _nugetApiKey = _cakeContext.EnvironmentVariable("NugetApiKey");
        _nugetQueryUrl= _cakeContext.EnvironmentVariable("NugetQueryUrl","https://www.myget.org/F/project-scorpio/api/v3/index.json");
        _nugetPushUrl = _cakeContext.EnvironmentVariable("NugetPushUrl");
        _symbolsPushUrl = _cakeContext.EnvironmentVariable("SymbolsPushUrl");
        _msbuildSettings=new DotNetMSBuildSettings()
										.SetFileVersion(_context.Version.GetFileVersion())
										.SetConfiguration(_context.Environment.Configuration)
										.SetVersion(_context.Version.GetVersion())
										.WithProperty("SourceLinkCreate","true");
    }

    public  void Clean()
    {
        _cakeContext.Information("Begin clean solution");
        _cakeContext.CleanDirectories("./**/src/**/bin");
        _cakeContext.CleanDirectories("./**/test/**/obj");
        _cakeContext.CleanDirectories("./**/src/**/bin");
        _cakeContext.CleanDirectories("./**/test/**/obj");
        _cakeContext.CleanDirectory(_context.ArtifactsPath);
    }

    public void Restore(){
        foreach (var item in _context.Soluations)
        {
            var settings=new DotNetRestoreSettings{
                Sources=new[]{"https://api.nuget.org/v3/index.json",_nugetQueryUrl}

            };
            _cakeContext.DotNetRestore(item.FullPath,new DotNetRestoreSettings{});
        }
    }

    public void Build(){
        var buildSettings=new DotNetBuildSettings{
                Configuration=_context.Environment.Configuration,
                MSBuildSettings=_msbuildSettings
            };
        foreach (var item in _context.Soluations)
        {
            _cakeContext.DotNetBuild(item.FullPath,buildSettings);
        }
    }

    public void Sonar(){
        var settings = GetSonarBeginSettings(); 
         _cakeContext.Sonar(ctx=>{
             Build();
             Test();
        },settings);
    }

    private SonarBeginSettings GetSonarBeginSettings(){
        var settings = new SonarBeginSettings() {
                Key="project-scorpio_scorpio",
                Organization="project-scorpio",
                Url="https://sonarcloud.io",
                Login="748862a8ccbf1654ac8b22ad5ae84b14778ba198",
                OpenCoverReportsPath ="**/coverage.opencover.xml"

            }; 
        
        if(_context.Environment.IsPullRequest){
            settings.PullRequestKey  =_context.Environment.PullRequestKey;
        }
        else if(!string.IsNullOrWhiteSpace(_context.Environment.Branch)){
            settings.Branch=_context.Environment.Branch;
        }
        return settings;
    }

    public void Test()
    {
        var testSettings=new DotNetTestSettings{ 
            Configuration=_context.Environment.Configuration,
            NoBuild= true,

        };
        var coverletSettings = new CoverletSettings {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.opencover,
        
        };
        foreach (var item in _context.Soluations)
        {
            _cakeContext.DotNetTest(item.FullPath,testSettings);
        }
    }

    public void Package(){
        var settings=new DotNetPackSettings
						{
						   Configuration = _context.Environment.Configuration,
						   OutputDirectory = _context.ArtifactsPath,
						   MSBuildSettings=_msbuildSettings,
                           IncludeSymbols =true,
                           NoBuild= true
						};
        foreach (var item in _context.Soluations)
        {
            _cakeContext.DotNetPack(item.FullPath,settings);
        }
    }

    public void Publish(){
        var packages=_cakeContext.GetFiles(_nugetRegex,new GlobberSettings{FilePredicate=f=>f.Path.FullPath.IndexOf(".symbols.")<0});
        var settings = new DotNetNuGetPushSettings
                         {
						   Source = _nugetPushUrl,
						   ApiKey = _nugetApiKey,
                           IgnoreSymbols=false,
                           SymbolApiKey=_nugetApiKey,
                           SymbolSource=_symbolsPushUrl,
                         };
        foreach (var item in packages)
        {
            _cakeContext.DotNetNuGetPush(item.FullPath,settings);
        }
    }
    
}
