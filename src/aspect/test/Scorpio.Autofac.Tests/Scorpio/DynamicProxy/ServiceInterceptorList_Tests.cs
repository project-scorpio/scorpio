using System;

using Scorpio.DynamicProxy.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.DynamicProxy
{
    public class ServiceInterceptorList_Test
    {
        [Fact]
        public void Add_Array()
        {
            var list = new ServiceInterceptorList();
            list.GetInterceptors(typeof(IProxiedService)).ShouldBeEmpty();
            Should.Throw<ArgumentNullException>(() => list.Add(typeof(IProxiedService), (Type)null));
            Should.Throw<ArgumentNullException>(() => list.Add(null, (Type)null));
            Should.Throw<ArgumentNullException>(() => list.Add(null, typeof(TestInterceptor)));
            list.Add(typeof(IProxiedService));
            list.GetInterceptors(typeof(IProxiedService)).ShouldBeEmpty();
            list.Add(typeof(IProxiedService), typeof(TestInterceptor));
            list.GetInterceptors(typeof(IProxiedService)).ShouldHaveSingleItem().ShouldBe(typeof(TestInterceptor));
        }

        [Fact]
        public void Add_TypeList()
        {
            var list = new ServiceInterceptorList();
            list.GetInterceptors(typeof(IProxiedService)).ShouldBeEmpty();
            Should.Throw<ArgumentNullException>(() => list.Add(typeof(IProxiedService), (Type)null));
            Should.Throw<ArgumentNullException>(() => list.Add(null, (ITypeList<IInterceptor>)null));
            Should.Throw<ArgumentNullException>(() => list.Add(null, new TypeList<IInterceptor>()));
            list.Add(typeof(IProxiedService), new TypeList<IInterceptor>());
            list.GetInterceptors(typeof(IProxiedService)).ShouldBeEmpty();
            list.Add(typeof(IProxiedService),new TypeList<IInterceptor>().Action(l=>l.Add<TestInterceptor>()));
            list.GetInterceptors(typeof(IProxiedService)).ShouldHaveSingleItem().ShouldBe(typeof(TestInterceptor));
        }
    }
}
