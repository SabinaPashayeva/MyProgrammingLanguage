using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class MultiplyExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public MultiplyExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public Expression Interpret(Context context)
        {
            return Expression.Multiply(leftExpression.Interpret(context),
                                  rightExpression.Interpret(context));
        }
    }
}
