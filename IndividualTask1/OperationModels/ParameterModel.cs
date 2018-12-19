using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class ParameterModel : IExpression
    {
        public ParameterExpression Parameter { get; private set; }
        public string ParameterName { get; private set; }

        public ParameterModel()
        {
        }

        public ParameterModel(string name)
        {
            ParameterName = name;
        }

        public void SetParameter(ParameterExpression param)
        {
            if (param != null) Parameter = param;
        }

        public Expression Interpret()
        {
            return Parameter;
        }
    }
}