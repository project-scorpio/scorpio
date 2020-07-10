namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFilterRequest
    {
        /// <summary>
        /// 
        /// </summary>
        string Where { get; }

        /// <summary>
        /// 
        /// </summary>
        object[] Parameters { get; }
    }

}
