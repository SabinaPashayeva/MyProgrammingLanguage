using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class DivideModel : NonTerminalExpression, IExpression
    {
        public DivideModel()
        {
        }

        public DivideModel(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Divide(LeftExpression.Interpret(),
                                  RightExpression.Interpret());
        }
    }
}
