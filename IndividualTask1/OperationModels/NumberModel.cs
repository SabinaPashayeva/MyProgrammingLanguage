using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class NumberModel : IExpression
    { 
        public double Constant { get; set; }

        public NumberModel()
        {
        }

        public NumberModel(double constant)
        {
            this.Constant = constant;
        }

        public Expression Interpret()
        {
            return Expression.Constant(Constant);
        }
    }
}
