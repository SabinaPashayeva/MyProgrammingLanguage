using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class AddExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public AddExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public Expression Interpret(Context context)
        {
            return Expression.Add(leftExpression.Interpret(context),
                                  rightExpression.Interpret(context));
        }
    }
}
