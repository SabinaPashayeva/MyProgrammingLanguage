using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class PowerExpression : TerminalExpression, IExpression
    {
        public PowerExpression()
        {
        }

        public PowerExpression(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Power(leftExpression.Interpret(),
                                  rightExpression.Interpret());
        }
    }
}
