using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class ParameterModel : IExpression
    {
        public ParameterExpression Parameter { get; set; }
        public string ParameterName { get; set; }

        public ParameterModel()
        {
        }

        public ParameterModel(ParameterExpression ex)
        {
            Parameter = ex;
        }

        public Expression Interpret()
        {
            return Parameter;
        }
    }
}