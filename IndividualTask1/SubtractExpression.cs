using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class SubtractExpression : TerminalExpression, IExpression
    {
        public SubtractExpression() 
        { 
        }

        public SubtractExpression(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Subtract(leftExpression.Interpret(),
                                  rightExpression.Interpret());
        }
    }
}
