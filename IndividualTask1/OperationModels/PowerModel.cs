using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class PowerModel : NonTerminalExpression, IExpression
    {
        public PowerModel()
        {
        }

        public PowerModel(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Power(LeftExpression.Interpret(),
                                  RightExpression.Interpret());
        }
    }
}
