using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DynamicProxy.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DynamicProxy
{
    public class ProxyConventionalAction_Tests
    {
        [Fact]
        public void Action()
        {
            var action = new ProxyConventionalAction();
            var services = new ServiceCollection();
            var types = new List<Type> { typeof(TestProxiedService) };
            var interceptor = new TypeList<IInterceptor>();
            Expression<Func<Type, bool>> typePredicate = t => true;
            interceptor.Add<TestInterceptor>();
            var context = new ProxyConventionalActionContext( services, types, typePredicate, interceptor);
            Should.NotThrow(() => action.Action(context));
            services.GetSingletonInstance<ServiceInterceptorList>().GetInterceptors(typeof(TestProxiedService)).ShouldHaveSingleItem().ShouldBe(typeof(TestInterceptor));

        }
    }
}
