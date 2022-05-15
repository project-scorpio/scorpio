using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using AspectCore.DynamicProxy;

namespace Scorpio.DynamicProxy
{
    internal class AspectCoreMethodInvocation : IMethodInvocation
    {
        private readonly AspectContext _context;
        private readonly AspectDelegate _next;
        private readonly Lazy<IReadOnlyDictionary<string, object>> _lazyArgumentsDictionary;
        private readonly Lazy<Type[]> _genericArguments;

        public AspectCoreMethodInvocation(AspectContext context, AspectDelegate next)
        {
            _context = Check.NotNull(context, nameof(context));
            _next = Check.NotNull(next, nameof(next));
            _lazyArgumentsDictionary = new Lazy<IReadOnlyDictionary<string, object>>(GetArgumentsDictionary);
            _genericArguments = new Lazy<Type[]>(GetGenericArguments);
        }
        public object[] Arguments => _context.Parameters;
        public IReadOnlyDictionary<string, object> ArgumentsDictionary => _lazyArgumentsDictionary.Value;
        public Type[] GenericArguments => _genericArguments.Value;
        public object TargetObject => _context.Implementation;
        public MethodInfo Method => _context.ImplementationMethod;
        public object ReturnValue { get => _context.ReturnValue; set => _context.ReturnValue = value; }

        public Task ProceedAsync() => _context.Invoke(_next);

        private IReadOnlyDictionary<string, object> GetArgumentsDictionary()
        {
            var dict = new Dictionary<string, object>();

            var methodParameters = Method.GetParameters();
            for (var i = 0; i < methodParameters.Length; i++)
            {
                dict[methodParameters[i].Name] = _context.Parameters[i];
            }
            return dict;
        }

        private Type[] GetGenericArguments() => Method.IsGenericMethod ? Method.GetGenericArguments() : new Type[0];
    }
}
