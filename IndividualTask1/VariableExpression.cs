using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class VariableExpression : IExpression
    {
        public ParameterExpression Parameter { get; set; }

        public VariableExpression()
        {
        }

        public VariableExpression(ParameterExpression ex)
        {
            Parameter = ex;
        }

        public Expression Interpret()
        {
            return Parameter;
        }
    }
}