namespace Scorpio.Data
{
    //TODO: Move to Scorpio.Data.ObjectExtending namespace at 4.0?

    /// <summary>
    /// 
    /// </summary>
    public interface IHasExtraProperties
    {
        /// <summary>
        /// 
        /// </summary>
        ExtraPropertyDictionary ExtraProperties { get; }
    }
}
