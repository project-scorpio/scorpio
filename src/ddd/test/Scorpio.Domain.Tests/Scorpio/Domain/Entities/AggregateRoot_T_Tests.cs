using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shouldly;

using Xunit;

namespace Scorpio.Domain.Entities
{
    public class AggregateRoot_T_Tests
    {
        [Fact]
        public void AddDomainEvent()
        {
            var ar = new TestAggregateRoot();
            ar.DomainEvents.ShouldBeEmpty();
            ar.AddDomainEvent(1);
            ar.DomainEvents.ShouldContain(1);
            ar.DomainEvents.ShouldHaveSingleItem();
            ar.ClearDomainEvents();
            ar.DomainEvents.ShouldBeEmpty();

        }

        private class TestAggregateRoot : AggregateRoot<int>
        {
          
         
            public void AddDomainEvent(int value) => base.AddDomainEvent(value);
        }
    }
}
