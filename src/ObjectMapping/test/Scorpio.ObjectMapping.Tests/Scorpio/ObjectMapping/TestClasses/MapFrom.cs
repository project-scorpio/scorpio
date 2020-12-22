﻿namespace Scorpio.ObjectMapping.TestClasses
{
    public class MapFromSource
    {
    }

    public class MapFromDest : IMapFrom<MapFromSource>
    {
        public MapFromDest(MapFromSource source) => Source = source;

        public MapFromSource Source { get; }

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
