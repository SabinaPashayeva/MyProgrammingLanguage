using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class SubtractExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public SubtractExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public Expression Interpret(Context context)
        {
            return Expression.Subtract(leftExpression.Interpret(context),
                                  rightExpression.Interpret(context));
        }
    }
}
