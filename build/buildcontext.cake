#load "./version.cake"
public class BuildContext
{
    public BuildContext(ICakeContext context) 
    {
        Context = context;
        Version=new BuildVersion(context);
        Environment=new BuildEnvironment(context);
        Projects = Context.GetDirectories("./**/src/*");
		TestProjects = Context.GetDirectories("./**/test/*");
		ProjectFiles = Context.GetFiles("./**/src/*/*.csproj");
		TestProjectFiles = Context.GetFiles("./**/test/*/*.csproj");
        ArtifactsPath=context.Directory("./artifacts");
    }

    public ICakeContext Context{get;}

    public BuildVersion Version { get;}
    
    public BuildEnvironment Environment{get;}

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

    public bool IsPublish=>(_buildSystem.IsRunningOnAppVeyor 
                            && _appVeyor.Environment.Repository.Tag.IsTag) ||(IsLocalBuild && Configuration=="Release");

    public IAppVeyorProvider AppVeyor => _appVeyor;

    public BuildSystem BuildSystem => _buildSystem;

    public ICakeContext Context => _context;
}