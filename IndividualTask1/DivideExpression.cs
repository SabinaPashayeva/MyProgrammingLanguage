using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class DivideExpression : TerminalExpression, IExpression
    {
        public DivideExpression()
        {
        }

        public DivideExpression(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Divide(leftExpression.Interpret(),
                                  rightExpression.Interpret());
        }
    }
}
