namespace Scorpio.ObjectMapping.TestClasses
{
    public class MapToSource : IMapTo<MapToDest>
    {
        public MapToDest MapTo() => new MapToDest();
        public void MapTo(MapToDest destination)
        {
            // Method intentionally left empty.
        }

    }

    public class MapToDest
    {

    }
}
