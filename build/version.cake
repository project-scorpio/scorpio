#load "./index.cake"

using System.Xml;

public class BuildVersion
{
    private readonly BuildContext _context;

    public BuildVersion(BuildContext context)
	{
        _context = context;
		var versionFile = context.Context.File("./build/versions.props");
		var content = System.IO.File.ReadAllText(versionFile.Path.FullPath);
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(content);
		Major =int.Parse(doc.DocumentElement.SelectSingleNode("/Project/PropertyGroup/VersionMajor").InnerText);
		Minor = int.Parse(doc.DocumentElement.SelectSingleNode("/Project/PropertyGroup/VersionMinor").InnerText);
		Patch = int.Parse(doc.DocumentElement.SelectSingleNode("/Project/PropertyGroup/VersionPatch").InnerText);
		Suffix = doc.DocumentElement.SelectSingleNode("/Project/PropertyGroup/VersionSuffix").InnerText;
		Build = context.Context.BuildSystem().IsRunningOnAppVeyor?context.Context.AppVeyor().Environment.Build.Number:Util.CreateStamp();
    }
	
	public int Major { get; set; }
	public int Minor { get; set; }
	public int Patch { get; set; }
    public int Build { get; set; }
	public string Suffix { get; set; }

    public string GetVersionPrefix() => $"{Major}.{Minor}.{Patch}";

    public string GetFileVersion() => $"{GetVersionPrefix()}.{Build}";

    public string GetVersionSuffix()
	{
		var suffix= string.IsNullOrWhiteSpace( Suffix)? string.Empty : $"-{Suffix}";
		if(_context.Environment.IsDaily){
			suffix= $"{suffix}-dev.{Build}";
		}
		return suffix;
	}

    public string GetVersion() => $"{GetVersionPrefix()}{GetVersionSuffix()}";
}
