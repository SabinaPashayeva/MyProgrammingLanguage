using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    public class RightStatementParser
    {
        private static readonly Regex stringRegex = new Regex("(?<=\").*(?=\")");
        private readonly string _command;
        private readonly Type resultExpressionType;

        public RightStatementParser(string command, Type expessionType)
        {
            _command = command;
            resultExpressionType = expessionType;
        }

        public Expression GetRightExpression()
        {
            if (_command == string.Empty) return Expression.Constant(_command);

            if (TryParseString(_command, out string resultStr))
                return GetConstantExpression(resultStr);

            return GetNestedExpression(_command);
        }

        private bool TryParseString(string input, out string resultStr)
        {
            resultStr = GetMatch(stringRegex, input);

            if (resultStr == string.Empty) return false;

            return true;
        }

        private Expression GetConstantExpression(string command)
        {
            if (resultExpressionType == typeof(string))
                return Expression.Constant(command);

            if (double.TryParse(command, out double number))
                return Expression.Constant(number);

            return default(Expression);
        }

        private Expression GetNestedExpression(string command)
        {
            var expression = ExpressionTreeBuilder
                             .TransformToExpressionTree(command)
                             .Interpret();

            if (resultExpressionType == typeof(string))
                return Expression.Call(expression, typeof(double)
                                               .GetMethod("ToString",
                                                           new Type[] { }));
            return expression;
        }

        private string GetMatch(Regex regex, string input)
        {
            return regex.Match(input).Value;
        }

    }
}
