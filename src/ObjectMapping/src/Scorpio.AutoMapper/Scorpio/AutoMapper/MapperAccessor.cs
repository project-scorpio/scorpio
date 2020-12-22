using AutoMapper;

namespace Scorpio.AutoMapper
{
    internal class MapperAccessor : IMapperAccessor
    {
        public IMapper Mapper { get; set; }
    }
}