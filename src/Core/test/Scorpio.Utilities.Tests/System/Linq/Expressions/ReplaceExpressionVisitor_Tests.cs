
using Shouldly;

using Xunit;

namespace System.Linq.Expressions
{
    public class ReplaceExpressionVisitor_Tests
    {
        [Fact]
        public void VisitInvocation()
        {
            Expression<Func<double, double, bool>> func = (v1, v2) => v1 + v2 < 1000;
            var expression = Expression.Lambda<Func<bool>>(Expression.Invoke(func, Expression.Constant(200.2), Expression.Constant(300.0)));
            expression.Compile()().ShouldBeTrue();
            var visitor = new ReplaceExpressionVisitor(func, (Expression<Func<double, double, bool>>)((v1, v2) => v1 + v2 < 400));
            expression = visitor.Visit(expression) as Expression<Func<bool>>;
            expression.Compile()().ShouldBeFalse();
        }

    }
}
