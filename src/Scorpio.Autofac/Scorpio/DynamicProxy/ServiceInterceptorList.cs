using System;
using System.Collections.Generic;

using Scorpio.DynamicProxy;

namespace Scorpio
{
    internal class ServiceInterceptorList
    {
        private static readonly Lazy<ServiceInterceptorList>  _instance=new Lazy<ServiceInterceptorList>( System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        public static ServiceInterceptorList Empty=>_instance.Value;
        private readonly Dictionary<Type,ITypeList<IInterceptor>> _interceptors;

        public ServiceInterceptorList() => _interceptors = new Dictionary<Type, ITypeList<IInterceptor>>();

        public void Add(Type serviceType,ITypeList<IInterceptor> interceptors)
        {
            var list=_interceptors.GetOrAdd(serviceType,t=>new TypeList<IInterceptor>());
            interceptors.ForEach(t=>list.AddIfNotContains(t));
        }    

        public void Add(Type serviceType,params Type[] interceptorTypes)
        {
            var list=_interceptors.GetOrAdd(serviceType,t=>new TypeList<IInterceptor>());
            interceptorTypes.ForEach(t=>list.AddIfNotContains(t));
        }

        public ITypeList<IInterceptor> GetInterceptors(Type serviceType) => _interceptors.GetOrDefault(serviceType, t => new TypeList<IInterceptor>());
    }
}
