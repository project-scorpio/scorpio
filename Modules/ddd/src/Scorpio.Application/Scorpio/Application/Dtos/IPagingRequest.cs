using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Application.Dtos
{
    public interface IPagingRequest
    {
        int SkipCount { get; }

        int MaxResultCount { get; }
    }
}
