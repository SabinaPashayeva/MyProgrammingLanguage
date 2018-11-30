using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class DivideExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public DivideExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public Expression Interpret(Context context)
        {
            return Expression.Divide(leftExpression.Interpret(context),
                                  rightExpression.Interpret(context));
        }
    }
}
