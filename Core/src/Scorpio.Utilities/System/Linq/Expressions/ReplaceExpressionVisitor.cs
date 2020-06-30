namespace System.Linq.Expressions
{
    internal class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node == _oldValue)
            {
                return _newValue;
            }
            return base.VisitParameter(node);
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            if (node.Expression == _oldValue && _newValue is LambdaExpression lambda)
            {
                var buiders = lambda.Parameters.Zip(node.Arguments, (p, a) => new ReplaceExpressionVisitor(p, a));
                return buiders.Aggregate(lambda.Body, (e, b) => b.Visit(e));
            }
            return base.VisitInvocation(node);
        }
    }
}
