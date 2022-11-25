using System;
using System.Collections.Generic;

using Scorpio.DynamicProxy;

using Shouldly;

using Xunit;

namespace Scorpio.Aspects
{
    public class CrossCuttingConcerns_Test
    {
        private class TestAvoidDuplicateCrossCuttingConcerns : IAvoidDuplicateCrossCuttingConcerns
        {
            public List<string> AppliedCrossCuttingConcerns { get; }=new List<string>();
        }

        [Fact]
        public void AddApplied()
        {
            var testObj=new TestAvoidDuplicateCrossCuttingConcerns();
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.AddApplied(null)).ParamName.ShouldBe("obj");
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.AddApplied(testObj)).ParamName.ShouldBe("concerns");
            Should.NotThrow(() => CrossCuttingConcerns.AddApplied(testObj,"conertn"));
            testObj.AppliedCrossCuttingConcerns.ShouldHaveSingleItem().ShouldBe("conertn");
        }
        [Fact]
        public  void RemoveApplied()
        {
            var testObj = new TestAvoidDuplicateCrossCuttingConcerns();
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.RemoveApplied(null)).ParamName.ShouldBe("obj");
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.RemoveApplied(testObj)).ParamName.ShouldBe("concerns");

            Should.NotThrow(() => CrossCuttingConcerns.RemoveApplied(new object(), "conertn"));
            Should.NotThrow(() => CrossCuttingConcerns.AddApplied(testObj, "conertn"));
            testObj.AppliedCrossCuttingConcerns.ShouldHaveSingleItem().ShouldBe("conertn");
            Should.NotThrow(() => CrossCuttingConcerns.RemoveApplied(testObj, "conertn1"));
            testObj.AppliedCrossCuttingConcerns.ShouldHaveSingleItem().ShouldBe("conertn");
            Should.NotThrow(() => CrossCuttingConcerns.RemoveApplied(testObj, "conertn"));
            testObj.AppliedCrossCuttingConcerns.ShouldBeEmpty();
        }

        [Fact]
        public void IsApplied()
        {
            var testObj = new TestAvoidDuplicateCrossCuttingConcerns();
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.IsApplied(null, null)).ParamName.ShouldBe("obj");
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.IsApplied(null, "concern")).ParamName.ShouldBe("obj");
            Should.Throw<ArgumentNullException>(() => CrossCuttingConcerns.IsApplied(testObj, null)).ParamName.ShouldBe("concern");
            Should.Throw<ArgumentException>(() => CrossCuttingConcerns.IsApplied(testObj, string.Empty)).ParamName.ShouldBe("concern");
            Should.NotThrow(() => CrossCuttingConcerns.IsApplied(new object(), "conertn")).ShouldBeFalse();
            Should.NotThrow(() => CrossCuttingConcerns.IsApplied(testObj, "conertn")).ShouldBeFalse();
            Should.NotThrow(() => CrossCuttingConcerns.AddApplied(testObj, "conertn"));

            Should.NotThrow(() => CrossCuttingConcerns.IsApplied(testObj, "conertn")).ShouldBeTrue();

        }

        [Fact]
        public void Applying()
        {
            var testObj = new TestAvoidDuplicateCrossCuttingConcerns();
            Should.NotThrow(() => CrossCuttingConcerns.IsApplied(testObj, "conertn")).ShouldBeFalse();
            using (Should.NotThrow(() => CrossCuttingConcerns.Applying(testObj, "conertn")))
            {
                Should.NotThrow(() => CrossCuttingConcerns.IsApplied(testObj, "conertn")).ShouldBeTrue();
            }
            Should.NotThrow(() => CrossCuttingConcerns.IsApplied(testObj, "conertn")).ShouldBeFalse();
        }

        [Fact]
        public void GetApplieds()
        {
            var stdObj = new object();
            var testObj = new TestAvoidDuplicateCrossCuttingConcerns();
            Should.NotThrow(() => CrossCuttingConcerns.GetApplieds(stdObj)).ShouldBeEmpty();
            Should.NotThrow(() => CrossCuttingConcerns.GetApplieds(testObj)).ShouldBeEmpty();
            Should.NotThrow(() => CrossCuttingConcerns.AddApplied(stdObj, "conertn"));
            Should.NotThrow(() => CrossCuttingConcerns.AddApplied(testObj, "conertn"));
            Should.NotThrow(() => CrossCuttingConcerns.GetApplieds(stdObj)).ShouldBeEmpty();
            Should.NotThrow(() => CrossCuttingConcerns.GetApplieds(testObj)).ShouldHaveSingleItem().ShouldBe("conertn");

        }
    }
}
