#load "./version.cake"
public class BuildContext
{
    public BuildContext(ICakeContext context) 
    {
        Context = context;
        Environment=new BuildEnvironment(context);
        Version=new BuildVersion(this);
        Projects = Context.GetDirectories("./**/src/*");
        Soluations = Context.GetFiles("./**/*.sln");
		TestProjects = Context.GetDirectories("./**/test/*");
		ProjectFiles = Context.GetFiles("./**/src/*/*.csproj");
		TestProjectFiles = Context.GetFiles("./**/test/*/*.csproj");
        ArtifactsPath=context.Directory("./artifacts");
    }

    public ICakeContext Context{get;}

    public BuildVersion Version { get;}
    
    public BuildEnvironment Environment{get;}

    public FilePathCollection Soluations { get;  }

    public DirectoryPathCollection Projects { get;  }

	public DirectoryPathCollection TestProjects { get; }
	
    public FilePathCollection ProjectFiles { get;}
	
    public FilePathCollection TestProjectFiles { get; }

    public DirectoryPath ArtifactsPath{get;}
}

public class BuildEnvironment
{
    private readonly ICakeContext _context;
    private readonly BuildSystem _buildSystem;

    private readonly IAppVeyorProvider _appVeyor;

    public BuildEnvironment(ICakeContext context)
    {
        _context=context;
        _buildSystem=_context.BuildSystem();
        _appVeyor=_buildSystem.AppVeyor;
    }
    public bool IsLocalBuild =>_buildSystem.IsLocalBuild;

    public bool IsRunningOnAppVeyor=>_buildSystem.IsRunningOnAppVeyor;

    public string Configuration=>_context.Argument("configuration",_buildSystem.IsLocalBuild?"Debug":"Release");

    public bool IsRelease=>_buildSystem.IsRunningOnAppVeyor && _appVeyor.Environment.Repository.Tag.IsTag;

    public bool IsPullRequest=>_appVeyor.Environment.PullRequest.IsPullRequest;

    public int PullRequestKey=>_appVeyor.Environment.PullRequest.Number;

    public bool IsDaily=>_buildSystem.IsRunningOnAppVeyor && _appVeyor.Environment.Repository.Branch.ToLower()=="dev";

    public string Branch=>_appVeyor.Environment.Repository.Branch;

    public bool IsPublish=>IsRelease||IsDaily;

    public IAppVeyorProvider AppVeyor => _appVeyor;

    public BuildSystem BuildSystem => _buildSystem;

    public ICakeContext Context => _context;
}