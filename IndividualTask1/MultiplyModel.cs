using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class MultiplyModel : NonTerminalExpression, IExpression
    {
        public MultiplyModel()
        {
        }

        public MultiplyModel(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Multiply(LeftExpression.Interpret(),
                                  RightExpression.Interpret());
        }
    }
}
