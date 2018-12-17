using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    public class VariableAssignModel : IExpression
    {
        private static readonly Regex nameRegex = new Regex("[a-zA-Z]+(?=\\s*<=)");
        private static readonly Regex rightValueRegex = new Regex("(?<=<=).+");

        public static Dictionary<string, ParameterExpression> Parameters { get; private set; }

        private string VariableName { get; set; }
        private string RightStatement { get; set; }

        public VariableAssignModel(string command)
        {
            VariableName = nameRegex.Match(command).Value;
            RightStatement = rightValueRegex.Match(command).Value;
        }

        static VariableAssignModel()
        {
            Parameters = new Dictionary<string, ParameterExpression>();
        }

        private void AddToDictionary(string name, ParameterExpression parameter)
        {
            if (parameter == null) return;

            if (Parameters.ContainsKey(name))
            {
                Parameters[name] = parameter;
                return;
            }

            Parameters.Add(name, parameter);
        }

        public Expression Interpret()
        {
            var parameter = Expression.Parameter(typeof(double));

            AddToDictionary(VariableName, parameter);
            var rigthValueParser = new RightStatementParser(RightStatement, typeof(double));

            return Expression.Assign(parameter, rigthValueParser.GetRightExpression());
        }
    }
}
