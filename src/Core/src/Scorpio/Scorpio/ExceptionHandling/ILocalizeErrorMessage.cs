using Scorpio.Localization;

namespace Scorpio.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILocalizeErrorMessage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string LocalizeMessage(LocalizationContext context);
    }
}