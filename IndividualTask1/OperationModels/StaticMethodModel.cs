using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    public class StaticMethodModel : IExpression
    {
        private static readonly Regex nameRegex = new Regex("[a-zA-Z]+(?=\\s*=>)");
        private static readonly Regex rightValueRegex = new Regex("(?<==>).+");

        private MethodInfo CurrentMethod { get; set; }
        private string RightValue { get; set; }

        public StaticMethodModel(string command)
        {
            var methodName = nameRegex.Match(command).Value;

            var staticMethods = new AdditionalMethods();
            if (staticMethods.ContainsKey(methodName))
                CurrentMethod = staticMethods[methodName];

            RightValue = rightValueRegex.Match(command).Value;
        }

        public Expression Interpret()
        {
            var rigthValueParser = new RightStatementParser(RightValue, typeof(string));

            return Expression.Call(CurrentMethod, rigthValueParser.GetRightExpression());
        }
    }
}
