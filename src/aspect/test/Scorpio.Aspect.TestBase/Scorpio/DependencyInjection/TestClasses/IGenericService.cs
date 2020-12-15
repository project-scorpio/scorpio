using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DependencyInjection.TestClasses
{
    public  interface IGenericService<T>
    {
    }

    public class GenericService<T> : IGenericService<T>
    {

    }
}
