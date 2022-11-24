
using AutoMapper;

using Scorpio.AutoMapper.TestClasses;

namespace Scorpio.AutoMapper
{
    public class TestProfile:Profile
    {
        public TestProfile() => CreateMap<ProfiledMapSource, ProfiledMapDestination>().Ignore(d => d.IgnoreValue);
    }
}
