namespace Scorpio.DependencyInjection.TestClasses
{
    public class ReadOnlyPropertyInjectionService:ITransientDependency
    {
        public PropertyService PropertyService { get; }
    }
}
