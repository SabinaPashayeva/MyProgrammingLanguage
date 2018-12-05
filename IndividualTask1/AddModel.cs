using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class AddModel : NonTerminalExpression, IExpression
    {
        public AddModel()
        {
        }

        public AddModel(IExpression left, IExpression right) : base(left, right)
        {
        }

        public Expression Interpret()
        {
            return Expression.Add(LeftExpression.Interpret(),
                                  RightExpression.Interpret());
        }
    }
}
