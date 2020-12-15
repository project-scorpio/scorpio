using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DynamicProxy.TestClasses
{
    public interface IProxiedService
    {
        void InterfaceMethod(int intValue,string stringValue);
    }
}
