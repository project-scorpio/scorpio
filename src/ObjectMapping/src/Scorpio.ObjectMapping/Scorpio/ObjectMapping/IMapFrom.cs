namespace Scorpio.ObjectMapping
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public interface IMapFrom<in TSource>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        void MapFrom(TSource source);
    }
}