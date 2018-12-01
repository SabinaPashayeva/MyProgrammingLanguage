using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class MultiplyExpression : TerminalExpression, IExpression
    {
        public MultiplyExpression()
        {
        }

        public MultiplyExpression(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Multiply(leftExpression.Interpret(),
                                  rightExpression.Interpret());
        }
    }
}
