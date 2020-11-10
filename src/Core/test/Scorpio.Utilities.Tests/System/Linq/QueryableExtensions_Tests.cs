using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace System.Linq
{
    public class QueryableExtensions_Tests
    {
        private static readonly IQueryable<string> _sourceList = new List<string> { "Item1", "Item2", "Item3", "Item4", "Item5", "Item6", "Item7" }.AsQueryable();

        [Fact]
        public void PageBy()
        {
            _sourceList.PageBy(2, 1).Count().ShouldBe(1);
            _sourceList.PageBy(2, 1).First().ShouldBe("Item3");
            _sourceList.PageBy(2, 10).Count().ShouldBe(5);
            _sourceList.OrderBy(s => s).PageBy<string, IOrderedQueryable<string>>(2, 2).Count().ShouldBe(2);
        }

        [Fact]
        public void WhereIf()
        {
            _sourceList.WhereIf(true, i => i == "Item2").ShouldHaveSingleItem().ShouldBe("Item2");
            _sourceList.WhereIf(false, i => i == "Item2").Count().ShouldBe(7);
            _sourceList.WhereIf(false, i => i == "Item2").ShouldBe(_sourceList);
            _sourceList.OrderBy(s => s).WhereIf<string, IOrderedQueryable<string>>(true, i => i == "Item2").ShouldHaveSingleItem().ShouldBe("Item2");
            _sourceList.OrderBy(s => s).WhereIf<string, IOrderedQueryable<string>>(false, i => i == "Item2").Count().ShouldBe(7);
            _sourceList.OrderBy(s => s).WhereIf<string, IOrderedQueryable<string>>(false, i => i == "Item2").ShouldBe(_sourceList);
            _sourceList.WhereIf(true, (item, i) => item == "Item2").ShouldHaveSingleItem().ShouldBe("Item2");
            _sourceList.WhereIf(false, (item, i) => item == "Item2").Count().ShouldBe(7);
            _sourceList.WhereIf(false, (item, i) => item == "Item2").ShouldBe(_sourceList);
            _sourceList.OrderBy(s => s).WhereIf<string, IOrderedQueryable<string>>(true, (item, i) => item == "Item2").ShouldHaveSingleItem().ShouldBe("Item2");
            _sourceList.OrderBy(s => s).WhereIf<string, IOrderedQueryable<string>>(false, (item, i) => item == "Item2").Count().ShouldBe(7);
            _sourceList.OrderBy(s => s).WhereIf<string, IOrderedQueryable<string>>(false, (item, i) => item == "Item2").ShouldBe(_sourceList);
        }
    }
}
