using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.ObjectMapping.TestClasses
{
    public class MapFromSource
    {
    }

    public class MapFromDest : IMapFrom<MapFromSource>
    {
        private readonly MapFromSource _source;

        public MapFromDest(MapFromSource source)
        {
            _source = source;
        }
        public void MapFrom(MapFromSource source)
        {
            // Method intentionally left empty.
        }
    }

    public class MapFromDestException : IMapFrom<MapFromSource>
    {

        public MapFromDestException()
        {
        }
        public void MapFrom(MapFromSource source)
        {
            // Method intentionally left empty.
        }
    }
}
