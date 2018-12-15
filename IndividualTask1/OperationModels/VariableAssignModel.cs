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
        public static Dictionary<string, ParameterExpression> Parameters;

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

        public Expression Interpret()
        {
            var parameter = Expression.Parameter(typeof(double));
            var rightExpression = GetExpression(RightStatement);
            Parameters.Add(VariableName, parameter);

            return Expression.Assign(parameter, rightExpression);
        }
    }
}
