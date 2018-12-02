using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class AddExpression : TerminalExpression, IExpression
    {
        public AddExpression()
        {
        }

        public AddExpression(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Add(leftExpression.Interpret(),
                                  rightExpression.Interpret());
        }
    }
}
