using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

using Scorpio;
using Scorpio.Localization;

using Shouldly;

using Xunit;
using Xunit.Sdk;

namespace System.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeHelper_Tests
    {
        private static readonly HashSet<Type> _floatingTypes = new HashSet<Type>
        {
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        public static IEnumerable<object[]> NonNullablePrimitiveTypes = new List<object[]>
        {
          new object[] { typeof(byte),               true},
          new object[] { typeof(short),              true},
          new object[] { typeof(int),                true},
          new object[] { typeof(long),               true},
          new object[] { typeof(sbyte),              true},
          new object[] { typeof(ushort),             true},
          new object[] { typeof(uint),               true},
          new object[] { typeof(ulong),              true},
          new object[] { typeof(bool),               true},
          new object[] { typeof(float),              true},
          new object[] { typeof(decimal),            true},
          new object[] { typeof(DateTime),           true},
          new object[] { typeof(DateTimeOffset),     true},
          new object[] { typeof(TimeSpan),           true},
          new object[] { typeof(Guid)      ,          true },
          new object[] { typeof(byte?),               false},
          new object[] { typeof(short?),              false},
          new object[] { typeof(int?),                false},
          new object[] { typeof(long?),               false},
          new object[] { typeof(sbyte?),              false},
          new object[] { typeof(ushort?),             false},
          new object[] { typeof(uint?),               false},
          new object[] { typeof(ulong?),              false},
          new object[] { typeof(bool?),               false},
          new object[] { typeof(float?),              false},
          new object[] { typeof(decimal?),            false},
          new object[] { typeof(DateTime?),           false},
          new object[] { typeof(DateTimeOffset?),     false},
          new object[] { typeof(TimeSpan?),           false},
          new object[] { typeof(Guid?)      ,          false },
          new object[] { typeof(string)      ,          false },
        };

        [Theory]
        [MemberData(nameof(NonNullablePrimitiveTypes))]
        public void IsNonNullablePrimitiveType(Type type, bool except)
        {
            TypeHelper.IsNonNullablePrimitiveType(type).ShouldBe(except);

        }

        public static IEnumerable<object[]> IsFuncData = new List<object[]>
        {
            new object[] { new object(),false},
            new object[] { null,false},
            new object[] { ()=>false,true}
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Theory]
        [MemberData(nameof(IsFuncData))]
        public void IsFunc(object obj, bool except)
        {
            TypeHelper.IsFunc(obj).ShouldBe(except);
        }

        [Fact]
        public void IsFunc_T()
        {
            TypeHelper.IsFunc<string>(null).ShouldBe(false);
            TypeHelper.IsFunc<string>(() => false).ShouldBe(false);
            TypeHelper.IsFunc<string>(() => "").ShouldBe(true);
        }

        public static IEnumerable<object[]> IsPrimitiveExtendedData(bool includeNullables, bool includeEnums)
        {
            yield return new object[] { includeNullables, includeEnums, typeof(byte), true };
            yield return new object[] { includeNullables, includeEnums, typeof(short), true };
            yield return new object[] { includeNullables, includeEnums, typeof(int), true };
            yield return new object[] { includeNullables, includeEnums, typeof(long), true };
            yield return new object[] { includeNullables, includeEnums, typeof(sbyte), true };
            yield return new object[] { includeNullables, includeEnums, typeof(ushort), true };
            yield return new object[] { includeNullables, includeEnums, typeof(uint), true };
            yield return new object[] { includeNullables, includeEnums, typeof(ulong), true };
            yield return new object[] { includeNullables, includeEnums, typeof(bool), true };
            yield return new object[] { includeNullables, includeEnums, typeof(float), true };
            yield return new object[] { includeNullables, includeEnums, typeof(decimal), true };
            yield return new object[] { includeNullables, includeEnums, typeof(DateTime), true };
            yield return new object[] { includeNullables, includeEnums, typeof(DateTimeOffset), true };
            yield return new object[] { includeNullables, includeEnums, typeof(TimeSpan), true };
            yield return new object[] { includeNullables, includeEnums, typeof(Guid), true };
            yield return new object[] { includeNullables, includeEnums, typeof(KnownColor), includeEnums };
            yield return new object[] { includeNullables, includeEnums, typeof(byte?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(short?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(int?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(long?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(sbyte?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(ushort?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(uint?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(ulong?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(bool?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(float?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(decimal?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(DateTime?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(DateTimeOffset?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(TimeSpan?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(Guid?), includeNullables };
            yield return new object[] { includeNullables, includeEnums, typeof(string), true };
            yield return new object[] { includeNullables, includeEnums, typeof(object), false };
            yield return new object[] { includeNullables, includeEnums, typeof(KnownColor?), includeEnums && includeNullables };

        }

        [Theory]
        [MemberData(nameof(IsPrimitiveExtendedData), false, false)]
        [MemberData(nameof(IsPrimitiveExtendedData), true, false)]
        [MemberData(nameof(IsPrimitiveExtendedData), false, true)]
        [MemberData(nameof(IsPrimitiveExtendedData), true, true)]
        public void IsPrimitiveExtended(bool includeNullables, bool includeEnums, Type type, bool except)
        {
            TypeHelper.IsPrimitiveExtended(type, includeNullables, includeEnums).ShouldBe(except);
        }

        [Fact]
        public void IsNullable()
        {
            TypeHelper.IsNullable(typeof(int?)).ShouldBe(true);
            TypeHelper.IsNullable(typeof(long?)).ShouldBe(true);
            TypeHelper.IsNullable(typeof(int)).ShouldBe(false);
            TypeHelper.IsNullable(typeof(long)).ShouldBe(false);
            TypeHelper.IsNullable(typeof(string)).ShouldBe(false);
        }

        [Fact]
        public void UnWrapNullable()
        {
            TypeHelper.UnWrapNullable(typeof(int)).ShouldBe(typeof(int));
            TypeHelper.UnWrapNullable(typeof(int?)).ShouldBe(typeof(int));
        }


        [Fact]
        public void IsEnumerable()
        {
            TypeHelper.IsEnumerable(
                typeof(IEnumerable<string>),
                out var itemType
            ).ShouldBeTrue();
            itemType.ShouldBe(typeof(string));

            TypeHelper.IsEnumerable(
                typeof(List<TypeHelper_Tests>),
                out itemType
            ).ShouldBeTrue();
            itemType.ShouldBe(typeof(TypeHelper_Tests));

            TypeHelper.IsEnumerable(
                typeof(TypeHelper_Tests),
                out itemType
            ).ShouldBeFalse();
            TypeHelper.IsEnumerable(
                typeof(int),
                out itemType,
                false
            ).ShouldBeFalse();
            TypeHelper.IsEnumerable(
                typeof(ArrayList),
                out itemType
                ).ShouldBeTrue();
            itemType.ShouldBe(typeof(object));

        }

        [Fact]
        public void IsDictionary()
        {
            //Dictionary<string, int>
            TypeHelper.IsDictionary(
                typeof(Dictionary<string, int>),
                out var keyType,
                out var valueType
            ).ShouldBeTrue();
            keyType.ShouldBe(typeof(string));
            valueType.ShouldBe(typeof(int));

            //MyDictionary
            TypeHelper.IsDictionary(
                typeof(TypeDictionary),
                out keyType,
                out valueType
            ).ShouldBeTrue();
            keyType.ShouldBe(typeof(Type));
            valueType.ShouldBe(typeof(Type));
            TypeHelper.IsDictionary(
                typeof(Hashtable),
                out keyType,
                out valueType
            ).ShouldBeTrue();
            keyType.ShouldBe(typeof(object));
            valueType.ShouldBe(typeof(object));

            //TypeHelper_Tests
            TypeHelper.IsDictionary(
                typeof(TypeHelper_Tests),
                out keyType,
                out valueType
            ).ShouldBeFalse();
        }

        [Fact]
        public void GetDefaultValue()
        {
            TypeHelper.GetDefaultValue(typeof(bool)).ShouldBe(false);
            TypeHelper.GetDefaultValue(typeof(int)).ShouldBe(0);
            TypeHelper.GetDefaultValue(typeof(string)).ShouldBeNull();
            TypeHelper.GetDefaultValue(typeof(StringComparison)).ShouldBe(StringComparison.CurrentCulture);
        }

        [Fact]
        public void GetDefaultValue_T()
        {
            TypeHelper.GetDefaultValue<bool>().ShouldBe(false);
            TypeHelper.GetDefaultValue<int>().ShouldBe(0);
            TypeHelper.GetDefaultValue<string>().ShouldBeNull();
            TypeHelper.GetDefaultValue<StringComparison>().ShouldBe(StringComparison.CurrentCulture);
        }



    }
}
