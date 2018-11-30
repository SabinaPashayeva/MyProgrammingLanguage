using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class PowerExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public PowerExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public Expression Interpret(Context context)
        {
            return Expression.Power(leftExpression.Interpret(context),
                                  rightExpression.Interpret(context));
        }
    }
}
