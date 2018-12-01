using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class NumberExpression : IExpression
    { 
        public double Constant { get; set; }

        public NumberExpression()
        {
        }

        public NumberExpression(double constant)
        {
            this.Constant = constant;
        }

        public Expression Interpret()
        {
            return Expression.Constant(Constant);
        }
    }
}
