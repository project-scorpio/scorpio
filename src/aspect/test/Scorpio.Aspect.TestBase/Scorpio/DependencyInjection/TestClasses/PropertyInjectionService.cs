namespace Scorpio.DependencyInjection.TestClasses
{
    public class PropertyInjectionService:ITransientDependency
    {
        public PropertyService  PropertyService { get; set; }
    }
}
