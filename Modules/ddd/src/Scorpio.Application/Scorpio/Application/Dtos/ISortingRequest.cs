using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// This interface is defined to standardize to request a sorted result.
    /// </summary>
    public interface ISortingRequest
    {
        /// <summary>
        /// Sorting information.
        /// Should include sorting field and optionally a direction (ASC or DESC)
        /// Can contain more than one field separated by comma (,).
        /// </summary>
        /// <example>
        /// Examples:
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Age DESC"
        /// </example>
        string Sorting { get; }
    }
}
