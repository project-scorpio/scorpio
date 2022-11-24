using System.Threading.Tasks;

namespace Scorpio.Middleware.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task PipelineRequestDelegate<in TPipelineContext>(TPipelineContext context);
}
