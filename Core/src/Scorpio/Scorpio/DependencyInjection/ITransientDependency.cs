using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DependencyInjection
{
    /// <summary>
    /// All classes implement this interface are automatically registered to dependency injection as transient service.
    /// </summary>
    public interface ITransientDependency: IDependency
    {

    }
}
