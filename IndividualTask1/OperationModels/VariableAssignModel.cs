using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    public class VariableAssignModel : IExpression
    {
        private string VariableName { get; set; }
        private string RightStatement { get; set; }
        static readonly Regex nameRegex = new Regex("[a-zA-Z]+(?=\\s*<=)");
        static readonly Regex rightValueRegex = new Regex("(?<=<=).+");
        static readonly Regex stringRegex = new Regex("(?<=\").*(?=\")");
        public static Dictionary<string, ParameterExpression> Parameters { get; private set; }

        public VariableAssignModel(string command)
        {
            VariableName = GetMatch(nameRegex, command);
            RightStatement = GetMatch(rightValueRegex, command);
        }

        static VariableAssignModel()
        {
            Parameters = new Dictionary<string, ParameterExpression>();
        }

        private Expression GetExpression(string value)
        {
            var tmpString = GetMatch(stringRegex, value);

            if (tmpString == string.Empty)
                return ExpressionTreeBuilder
                        .TransformToExpressionTree(value)
                        .Interpret();

            if (!double.TryParse(tmpString, out double number))
                return default(Expression);

            return Expression.Constant(number);
        }

        private string GetMatch(Regex regex, string input)
        {
            return regex.Match(input).Value;
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
            var rightExpression = GetExpression(RightStatement);
            AddToDictionary(VariableName, parameter);

            return Expression.Assign(parameter, rightExpression);
        }
    }
}
