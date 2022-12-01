using System.Diagnostics.CodeAnalysis;

namespace Scorpio.Data
{
    [ExcludeFromCodeCoverage]
    internal class SoftDelete : ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
