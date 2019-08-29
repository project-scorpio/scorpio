using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.DynamicProxy
{
    internal class TypeInterceptorMap
    {
        public Type InterceptorType { get; private set; }

        public List<Type> ServiceTypes { get; private set; } = new List<Type>();

        public void AddService(Type serviceType)
        {
            ServiceTypes.Add(serviceType);
        }

        internal TypeInterceptorMap(Type type)
        {
            InterceptorType = type;
        }



        public override int GetHashCode()
        {
            return InterceptorType.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TypeInterceptorMap other))
            {
                return false;
            }
            return other.InterceptorType.Equals(InterceptorType);
        }
    }
}
