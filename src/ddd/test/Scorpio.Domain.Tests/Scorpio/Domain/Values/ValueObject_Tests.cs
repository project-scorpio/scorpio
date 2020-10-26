using System.Collections.Generic;

using Scorpio.Data.Values;

using Shouldly;

using Xunit;

namespace Scorpio.Domain.Values
{
    public class ValueObject_Tests
    {
        [Fact]
        public void VO_Equals()
        {
            var orderDetail = new OrderDetail { ProductId = 10, Cost = 100, Count = 2 };
            orderDetail.Equals(new OrderDetail { ProductId = 10, Cost = 100, Count = 2 }).ShouldBeTrue();
            new OrderDetail { ProductId = 10, Cost = 100, Count = 2 }.Equals(new CertDetail { ProductId = 10, Cost = 100, Count = 2 }).ShouldBeFalse();
            new OrderDetail { ProductId = 10, Cost = 100, Count = 2 }.Equals(new OrderDetail { ProductId = 9, Cost = 100, Count = 2 }).ShouldBeFalse();
            var empty = new Empty();
            empty.Equals(new Empty()).ShouldBeTrue();
            new Empty().Equals(null).ShouldBeFalse();
            new OrderDetail { ProductId = 10, Cost = 100, Count = 2 }.GetHashCode().ShouldBe(23837272);
            new Order { Id = 10 }.GetHashCode().ShouldBe(922);
        }

        [Fact]
        public void OV_GetHashCode()
        {
            new Empty().GetHashCode().ShouldBe(31);
        }


        class Empty : ValueObject<Empty>
        {

        }

        class Order : ValueObject<Order>
        {
            public Address Address { get; set; }

            public HashSet<OrderDetail> OrderDetails { get; set; }

            public int Id { get; set; }

        }

        class OrderDetail : ValueObject<OrderDetail>
        {
            public int ProductId { get; set; }

            public int Count { get; set; }

            public decimal Cost { get; set; }
        }

        class CertDetail : ValueObject<CertDetail>
        {
            public int ProductId { get; set; }

            public int Count { get; set; }

            public decimal Cost { get; set; }
        }

        class Address : ValueObject<Address>
        {
            public Area Area { get; set; }

            public string DetailAddress { get; set; }

            public string Mobile { get; set; }
        }

        class Area : ValueObject<Area>
        {
            public string Country { get; set; }

            public string Province { get; set; }

            public string City { get; set; }
        }
    }
}
