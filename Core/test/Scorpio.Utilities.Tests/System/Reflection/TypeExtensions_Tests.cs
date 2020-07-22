﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Scorpio;
using Scorpio.Middleware.Pipeline;

using Shouldly;

using Xunit;

namespace System.Reflection
{
    public class TypeExtensions_Tests
    {
        [Fact]
        public void IsInNamespace()
        {
            Should.Throw<ArgumentNullException>(() => default(Type).IsInNamespace(null));
            Should.Throw<ArgumentNullException>(() => default(Type).IsInNamespace("System"));
            Should.Throw<ArgumentNullException>(() => typeof(string).IsInNamespace(null));
            Should.Throw<ArgumentNullException>(() => default(Type).IsInNamespaceOf<string>());
            Should.NotThrow(() => typeof(string).IsInNamespace("System")).ShouldBeTrue();
            Should.NotThrow(() => typeof(string).IsInNamespaceOf<int>()).ShouldBeTrue();
            Should.NotThrow(() => typeof(int).IsInNamespace("System")).ShouldBeTrue();
            Should.NotThrow(() => typeof(IServiceCollection).IsInNamespace("System")).ShouldBeFalse();
            Should.NotThrow(() => typeof(string).IsInNamespaceOf<IServiceCollection>()).ShouldBeFalse();

        }
        [Fact]
        public void IsAssignableTo()
        {
            Should.Throw<ArgumentNullException>(() => default(Type).IsAssignableTo(null));
            Should.Throw<ArgumentNullException>(() => default(Type).IsAssignableTo(typeof(IServiceCollection)));
            Should.Throw<ArgumentNullException>(() => typeof(IServiceCollection).IsAssignableTo(null));
            Should.Throw<ArgumentNullException>(() => default(Type).IsAssignableTo<string>());
            Should.NotThrow(() => typeof(ServiceCollection).IsAssignableTo<IServiceCollection>()).ShouldBeTrue();
            Should.NotThrow(() => typeof(ServiceCollection).IsAssignableTo(typeof(IServiceCollection))).ShouldBeTrue();
            Should.NotThrow(() => typeof(ServiceCollection).IsAssignableTo<IServiceProvider>()).ShouldBeFalse();
            Should.NotThrow(() => typeof(ServiceCollection).IsAssignableTo(typeof(IServiceProvider))).ShouldBeFalse();
        }

        [Fact]
        public void IsAssignableToGenericType()
        {
            Should.Throw<ArgumentNullException>(() => default(Type).IsAssignableToGenericType(null)).ParamName.ShouldBe("this");
            Should.Throw<ArgumentNullException>(() => default(Type).IsAssignableToGenericType(typeof(IEqualityComparer<>))).ParamName.ShouldBe("this");
            Should.Throw<ArgumentNullException>(() => typeof(List<>).IsAssignableToGenericType(null)).ParamName.ShouldBe("genericType");
            Should.NotThrow(() => typeof(TestPipelineBuilder).IsAssignableToGenericType(typeof(PipelineBuilder<>))).ShouldBeTrue();
            Should.NotThrow(() => typeof(TestPipelineBuilder).IsAssignableToGenericType(typeof(IPipelineBuilder<>))).ShouldBeTrue();
            Should.NotThrow(() => typeof(TestPipelineBuilder).IsAssignableToGenericType(typeof(IServiceProviderFactory<>))).ShouldBeFalse();
        }

        [Fact]
        public void IsStandardType()
        {
            Should.Throw<ArgumentNullException>(() => default(Type).IsStandardType()).ParamName.ShouldBe("this");
            Should.NotThrow(() => typeof(IServiceCollection).IsStandardType()).ShouldBeFalse();
            Should.NotThrow(() => typeof(IServiceProviderFactory<>).IsStandardType()).ShouldBeFalse();
            Should.NotThrow(() => typeof(PipelineBuilder<>).IsStandardType()).ShouldBeFalse();
            Should.NotThrow(() => typeof(List<>).IsStandardType()).ShouldBeFalse();
            Should.NotThrow(() => typeof(Stream).IsStandardType()).ShouldBeFalse();
            Should.NotThrow(() => typeof(TypeList).IsStandardType()).ShouldBeTrue();
            Should.NotThrow(() => typeof(string).IsStandardType()).ShouldBeTrue();
        }
    }
}
