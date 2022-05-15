namespace Scorpio.ObjectMapping
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDestination"></typeparam>
    public interface IMapTo<TDestination>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TDestination MapTo();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        void MapTo(TDestination destination);
    }
}
