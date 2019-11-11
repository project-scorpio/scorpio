using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Scorpio.DependencyInjection;

namespace Scorpio.AspNetCore.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITagHelperService
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTagHelper"></typeparam>
    public interface ITagHelperService<TTagHelper> :ITagHelperService where TTagHelper : TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 
        /// </summary>
        TTagHelper TagHelper { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Init(TagHelperContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        Task ProcessAsync(TagHelperContext context, TagHelperOutput output);
    }
}