using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class SubtractModel : NonTerminalExpression, IExpression
    {
        public SubtractModel() 
        { 
        }

        public SubtractModel(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Subtract(LeftExpression.Interpret(),
                                  RightExpression.Interpret());
        }
    }
}
