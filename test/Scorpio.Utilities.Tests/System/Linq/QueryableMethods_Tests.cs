using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using NSubstitute;

using Scorpio;

using Shouldly;

using Xunit;

namespace System.Linq
{

    public class QueryableMethods_Tests
    {
        [Fact]
        public void All()
        {
            QueryableMethods.All.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.All));
        }

        [Fact]
        public void AnyWithoutPredicate()
        {
            QueryableMethods.AnyWithoutPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Any));
            QueryableMethods.AnyWithoutPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.AnyWithoutPredicate.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void AnyWithPredicate()
        {
            QueryableMethods.AnyWithPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Any));
            QueryableMethods.AnyWithPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.AnyWithPredicate.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                });

        }

        [Fact]
        public void AsQueryable()
        {
            QueryableMethods.AsQueryable.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.AsQueryable));
            QueryableMethods.AsQueryable.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.AsQueryable.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }


        [Fact]
        public void Cast()
        {
            QueryableMethods.Cast.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Cast));
            QueryableMethods.Cast.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Cast.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");

        }

        [Fact]
        public void Concat()
        {
            QueryableMethods.Concat.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Concat));
            QueryableMethods.Concat.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Concat.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source1");
                    args[1].ShouldNotBeNull().Name.ShouldBe("source2");
                });
        }


        [Fact]
        public void Contains()
        {
            QueryableMethods.Contains.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Contains));
            QueryableMethods.Contains.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Contains.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("item");
                });

        }

        [Fact]
        public void CountWithoutPredicate()
        {
            QueryableMethods.CountWithoutPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Count));
            QueryableMethods.CountWithoutPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.CountWithoutPredicate.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");

        }

        [Fact]
        public void CountWithPredicate()
        {
            QueryableMethods.CountWithPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Count));
            QueryableMethods.CountWithPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.CountWithPredicate.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                });
        }

        [Fact]
        public void DefaultIfEmptyWithoutArgument()
        {
            QueryableMethods.DefaultIfEmptyWithoutArgument.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.DefaultIfEmpty));
            QueryableMethods.DefaultIfEmptyWithoutArgument.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.DefaultIfEmptyWithoutArgument.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");

        }

        [Fact]
        public void DefaultIfEmptyWithArgument()
        {
            QueryableMethods.DefaultIfEmptyWithArgument.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.DefaultIfEmpty));
            QueryableMethods.DefaultIfEmptyWithArgument.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.DefaultIfEmptyWithArgument.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("defaultValue");
                });
        }

        [Fact]
        public void Distinct()
        {
            QueryableMethods.Distinct.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Distinct));
            QueryableMethods.Distinct.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Distinct.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void ElementAt()
        {
            QueryableMethods.ElementAt.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.ElementAt));
            QueryableMethods.ElementAt.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.ElementAt.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("index");
                });
        }

        [Fact]
        public void ElementAtOrDefault()
        {
            QueryableMethods.ElementAtOrDefault.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.ElementAtOrDefault));
            QueryableMethods.ElementAtOrDefault.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.ElementAtOrDefault.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("index");
                });
        }

        [Fact]
        public void Except()
        {
            QueryableMethods.Except.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Except));
            QueryableMethods.Except.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Except.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source1");
                    args[1].ShouldNotBeNull().Name.ShouldBe("source2");
                });
        }

        [Fact]
        public void FirstWithoutPredicate()
        {
            QueryableMethods.FirstWithoutPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.First));
            QueryableMethods.FirstWithoutPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.FirstWithoutPredicate.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void FirstWithPredicate()
        {
            QueryableMethods.FirstWithPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.First));
            QueryableMethods.FirstWithPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.FirstWithPredicate.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                });
        }

        [Fact]
        public void FirstOrDefaultWithoutPredicate()
        {
            QueryableMethods.FirstOrDefaultWithoutPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.FirstOrDefault));
            QueryableMethods.FirstOrDefaultWithoutPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.FirstOrDefaultWithoutPredicate.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void FirstOrDefaultWithPredicate()
        {
            QueryableMethods.FirstOrDefaultWithPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.FirstOrDefault));
            QueryableMethods.FirstOrDefaultWithPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.FirstOrDefaultWithPredicate.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                });
        }

        [Fact]
        public void GroupByWithKeySelector()
        {
            QueryableMethods.GroupByWithKeySelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.GroupBy));
            QueryableMethods.GroupByWithKeySelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.GroupByWithKeySelector.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("keySelector");
                });
        }

        [Fact]
        public void GroupByWithKeyElementSelector()
        {
            QueryableMethods.GroupByWithKeyElementSelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.GroupBy));
            QueryableMethods.GroupByWithKeyElementSelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.GroupByWithKeyElementSelector.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(3);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("keySelector");
                    args[2].ShouldNotBeNull().Name.ShouldBe("elementSelector");
                });
        }

        [Fact]
        public void GroupByWithKeyElementResultSelector()
        {
            QueryableMethods.GroupByWithKeyElementResultSelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.GroupBy));
            QueryableMethods.GroupByWithKeyElementResultSelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.GroupByWithKeyElementResultSelector.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(4);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("keySelector");
                    args[2].ShouldNotBeNull().Name.ShouldBe("elementSelector");
                    args[3].ShouldNotBeNull().Name.ShouldBe("resultSelector");
                });
        }

        [Fact]
        public void GroupByWithKeyResultSelector()
        {
            QueryableMethods.GroupByWithKeyResultSelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.GroupBy));
            QueryableMethods.GroupByWithKeyResultSelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.GroupByWithKeyResultSelector.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(3);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("keySelector");
                    args[2].ShouldNotBeNull().Name.ShouldBe("resultSelector");
                });
        }

        [Fact]
        public void GroupJoin()
        {
            QueryableMethods.GroupJoin.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.GroupJoin));
            QueryableMethods.GroupJoin.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.GroupJoin.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(5);
                    args[0].ShouldNotBeNull().Name.ShouldBe("outer");
                    args[1].ShouldNotBeNull().Name.ShouldBe("inner");
                    args[2].ShouldNotBeNull().Name.ShouldBe("outerKeySelector");
                    args[3].ShouldNotBeNull().Name.ShouldBe("innerKeySelector");
                    args[4].ShouldNotBeNull().Name.ShouldBe("resultSelector");
                });
        }

        [Fact]
        public void Intersect()
        {
            QueryableMethods.Intersect.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Intersect));
            QueryableMethods.Intersect.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Intersect.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source1");
                    args[1].ShouldNotBeNull().Name.ShouldBe("source2");
                });
        }

        [Fact]
        public void Join()
        {
            QueryableMethods.Join.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Join));
            QueryableMethods.Join.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Join.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(5);
                    args[0].ShouldNotBeNull().Name.ShouldBe("outer");
                    args[1].ShouldNotBeNull().Name.ShouldBe("inner");
                    args[2].ShouldNotBeNull().Name.ShouldBe("outerKeySelector");
                    args[3].ShouldNotBeNull().Name.ShouldBe("innerKeySelector");
                    args[4].ShouldNotBeNull().Name.ShouldBe("resultSelector");
                });
        }

        [Fact]
        public void LastWithoutPredicate()
        {
            QueryableMethods.LastWithoutPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Last));
            QueryableMethods.LastWithoutPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.LastWithoutPredicate.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void LastWithPredicate()
        {
            QueryableMethods.LastWithPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Last));
            QueryableMethods.LastWithPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.LastWithPredicate.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                });
        }

        [Fact]
        public void LastOrDefaultWithoutPredicate()
        {
            QueryableMethods.LastOrDefaultWithoutPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.LastOrDefault));
            QueryableMethods.LastOrDefaultWithoutPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.LastOrDefaultWithoutPredicate.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void LastOrDefaultWithPredicate()
        {
            QueryableMethods.LastOrDefaultWithPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.LastOrDefault));
            QueryableMethods.LastOrDefaultWithPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.LastOrDefaultWithPredicate.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                });
        }

        [Fact]
        public void LongCountWithoutPredicate()
        {
            QueryableMethods.LongCountWithoutPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.LongCount));
            QueryableMethods.LongCountWithoutPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.LongCountWithoutPredicate.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void LongCountWithPredicate()
        {
            QueryableMethods.LongCountWithPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.LongCount));
            QueryableMethods.LongCountWithPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.LongCountWithPredicate.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                });
        }

        [Fact]
        public void MaxWithoutSelector()
        {
            QueryableMethods.MaxWithoutSelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Max));
            QueryableMethods.MaxWithoutSelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.MaxWithoutSelector.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void MaxWithSelector()
        {
            QueryableMethods.MaxWithSelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Max));
            QueryableMethods.MaxWithSelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.MaxWithSelector.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("selector");
                });
        }

        [Fact]
        public void MinWithoutSelector()
        {
            QueryableMethods.MinWithoutSelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Min));
            QueryableMethods.MinWithoutSelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.MinWithoutSelector.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void MinWithSelector()
        {
            QueryableMethods.MinWithSelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Min));
            QueryableMethods.MinWithSelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.MinWithSelector.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("selector");
                });
        }

        [Fact]
        public void OfType()
        {
            QueryableMethods.OfType.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.OfType));
            QueryableMethods.OfType.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.OfType.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void OrderBy()
        {
            QueryableMethods.OrderBy.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.OrderBy));
            QueryableMethods.OrderBy.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.OrderBy.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("keySelector");
                });
        }

        [Fact]
        public void OrderByDescending()
        {
            QueryableMethods.OrderByDescending.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.OrderByDescending));
            QueryableMethods.OrderByDescending.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.OrderByDescending.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("keySelector");
                });
        }

        [Fact]
        public void Reverse()
        {
            QueryableMethods.Reverse.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Reverse));
            QueryableMethods.Reverse.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Reverse.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void Select()
        {
            QueryableMethods.Select.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Select));
            QueryableMethods.Select.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Select.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("selector");
                    args[1].ShouldNotBeNull().ParameterType.GetGenericArguments()[0].GetGenericArguments().Length.ShouldBe(2);
                });
        }

        [Fact]
        public void SelectManyWithoutCollectionSelector()
        {
            QueryableMethods.SelectManyWithoutCollectionSelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.SelectMany));
            QueryableMethods.SelectManyWithoutCollectionSelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.SelectManyWithoutCollectionSelector.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("selector");
                });
        }

        [Fact]
        public void SelectManyWithCollectionSelector()
        {
            QueryableMethods.SelectManyWithCollectionSelector.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.SelectMany));
            QueryableMethods.SelectManyWithCollectionSelector.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.SelectManyWithCollectionSelector.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(3);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("collectionSelector");
                    args[2].ShouldNotBeNull().Name.ShouldBe("resultSelector");
                });
        }

        [Fact]
        public void SingleWithoutPredicate()
        {
            QueryableMethods.SingleWithoutPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Single));
            QueryableMethods.SingleWithoutPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.SingleWithoutPredicate.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void SingleWithPredicate()
        {
            QueryableMethods.SingleWithPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Single));
            QueryableMethods.SingleWithPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.SingleWithPredicate.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                });
        }

        [Fact]
        public void SingleOrDefaultWithoutPredicate()
        {
            QueryableMethods.SingleOrDefaultWithoutPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.SingleOrDefault));
            QueryableMethods.SingleOrDefaultWithoutPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.SingleOrDefaultWithoutPredicate.ShouldNotBeNull().GetParameters().ShouldHaveSingleItem().Name.ShouldBe("source");
        }

        [Fact]
        public void SingleOrDefaultWithPredicate()
        {
            QueryableMethods.SingleOrDefaultWithPredicate.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.SingleOrDefault));
            QueryableMethods.SingleOrDefaultWithPredicate.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.SingleOrDefaultWithPredicate.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                });
        }

        [Fact]
        public void Skip()
        {
            QueryableMethods.Skip.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Skip));
            QueryableMethods.Skip.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Skip.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("count");
                });
        }

        [Fact]
        public void SkipWhile()
        {
            QueryableMethods.SkipWhile.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.SkipWhile));
            QueryableMethods.SkipWhile.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.SkipWhile.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                    args[1].ShouldNotBeNull().ParameterType.GetGenericArguments()[0].GetGenericArguments().Length.ShouldBe(2);
                });
        }

        [Fact]
        public void Take()
        {
            QueryableMethods.Take.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Take));
            QueryableMethods.Take.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Take.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("count");
                });
        }

        [Fact]
        public void TakeWhile()
        {
            QueryableMethods.TakeWhile.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.TakeWhile));
            QueryableMethods.TakeWhile.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.TakeWhile.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                    args[1].ShouldNotBeNull().ParameterType.GetGenericArguments()[0].GetGenericArguments().Length.ShouldBe(2);
                });
        }

        [Fact]
        public void ThenBy()
        {
            QueryableMethods.ThenBy.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.ThenBy));
            QueryableMethods.ThenBy.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.ThenBy.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("keySelector");
                });
        }

        [Fact]
        public void ThenByDescending()
        {
            QueryableMethods.ThenByDescending.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.ThenByDescending));
            QueryableMethods.ThenByDescending.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.ThenByDescending.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("keySelector");
                });
        }

        [Fact]
        public void Union()
        {
            QueryableMethods.Union.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Union));
            QueryableMethods.Union.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Union.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source1");
                    args[1].ShouldNotBeNull().Name.ShouldBe("source2");
                });
        }

        [Fact]
        public void Where()
        {
            QueryableMethods.Where.ShouldNotBeNull().Name.ShouldBe(nameof(Queryable.Where));
            QueryableMethods.Where.ShouldNotBeNull().IsGenericMethodDefinition.ShouldBeTrue();
            QueryableMethods.Where.ShouldNotBeNull().GetParameters()
                .Action(args =>
                {
                    args.Length.ShouldBe(2);
                    args[0].ShouldNotBeNull().Name.ShouldBe("source");
                    args[1].ShouldNotBeNull().Name.ShouldBe("predicate");
                    args[1].ShouldNotBeNull().ParameterType.GetGenericArguments()[0].GetGenericArguments().Length.ShouldBe(2);
                });
        }

        public static IEnumerable<object[]> TypeExcepts()
        {
            yield return new object[] { typeof(int), true };
            yield return new object[] { typeof(int?), true };
            yield return new object[] { typeof(long), true };
            yield return new object[] { typeof(long?), true };
            yield return new object[] { typeof(float), true };
            yield return new object[] { typeof(float?), true };
            yield return new object[] { typeof(double), true };
            yield return new object[] { typeof(double?), true };
            yield return new object[] { typeof(decimal), true };
            yield return new object[] { typeof(decimal?), true };
        }

        [Theory]
        [MemberData(nameof(TypeExcepts))]
        public void IsAverageWithoutSelector(Type type, bool except)
        {
            var method = GetMethod(
                    nameof(Queryable.Average), 0, types => new[] { typeof(IQueryable<>).MakeGenericType(type) });
            QueryableMethods.IsAverageWithoutSelector(method).ShouldBe(except);
        }


        [Theory]
        [MemberData(nameof(TypeExcepts))]
        public void IsAverageWithSelector(Type type, bool except)
        {
            var method = GetMethod(
                    nameof(Queryable.Average), 1,
                    types => new[]
                    {
                    typeof(IQueryable<>).MakeGenericType(types[0]),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(types[0], type))
                    });
            QueryableMethods.IsAverageWithSelector(method).ShouldBe(except);

        }

        [Theory]
        [MemberData(nameof(TypeExcepts))]
        public void IsSumWithoutSelector(Type type, bool except)
        {
            var method = GetMethod(
                    nameof(Queryable.Sum), 0, types => new[] { typeof(IQueryable<>).MakeGenericType(type) });
            QueryableMethods.IsSumWithoutSelector(method).ShouldBe(except);

        }

        [Theory]
        [MemberData(nameof(TypeExcepts))]
        public void IsSumWithSelector(Type type, bool except)
        {
            var method = GetMethod(
                    nameof(Queryable.Sum), 1,
                    types => new[]
                    {
                    typeof(IQueryable<>).MakeGenericType(types[0]),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(types[0], type))
                    });
            QueryableMethods.IsSumWithSelector(method).ShouldBe(except);

        }

        [Theory]
        [MemberData(nameof(TypeExcepts))]
        public void GetAverageWithoutSelector(Type type, bool except)
        {
            var method = GetMethod(nameof(Queryable.Average), 0, types => new[] { typeof(IQueryable<>).MakeGenericType(type) });
            QueryableMethods.GetAverageWithoutSelector(type).ShouldBe(method);

        }

        [Theory]
        [MemberData(nameof(TypeExcepts))]
        public void GetAverageWithSelector(Type type, bool except)
        {
            var method = GetMethod(
                nameof(Queryable.Average), 1,
                types => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(types[0]),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(types[0], type))
                });

            QueryableMethods.GetAverageWithSelector(type).ShouldBe(method);

        }

        [Theory]
        [MemberData(nameof(TypeExcepts))]
        public void GetSumWithoutSelector(Type type, bool except)
        {
            var method = GetMethod(nameof(Queryable.Sum), 0, types => new[] { typeof(IQueryable<>).MakeGenericType(type) });
            QueryableMethods.GetSumWithoutSelector(type).ShouldBe(method);

        }

        [Theory]
        [MemberData(nameof(TypeExcepts))]
        public void GetSumWithSelector(Type type, bool except)
        {
            var method = GetMethod(
                nameof(Queryable.Sum), 1,
                types => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(types[0]),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(types[0], type))
                });
            QueryableMethods.GetSumWithSelector(type).ShouldBe(method);

        }

        private static MethodInfo GetMethod(string name, int genericParameterCount, Func<Type[], Type[]> parameterGenerator)
                  => typeof(Queryable).GetMethods().Where(m => m.Name == name).Single(
                      mi => ((genericParameterCount == 0 && !mi.IsGenericMethod)
                              || (mi.IsGenericMethod && mi.GetGenericArguments().Length == genericParameterCount))
                          && mi.GetParameters().Select(e => e.ParameterType).SequenceEqual(
                              parameterGenerator(mi.IsGenericMethod ? mi.GetGenericArguments() : Array.Empty<Type>())));

    }
}
