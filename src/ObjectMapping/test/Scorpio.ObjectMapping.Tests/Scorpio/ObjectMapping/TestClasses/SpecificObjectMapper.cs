namespace Scorpio.ObjectMapping.TestClasses
{
    public class SpecificObjectMapper : IObjectMapper<SpecificObjectMapperSource, SpecificObjectMapperDest>
    {
        public SpecificObjectMapperDest Map(SpecificObjectMapperSource source) => new SpecificObjectMapperDest();
        public SpecificObjectMapperDest Map(SpecificObjectMapperSource source, SpecificObjectMapperDest destination) =>destination;
    }

    public class SpecificObjectMapperSource
    {

    }

    public class SpecificObjectMapperDest
    {

    }
}
