using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    public class StaticMethodModel : IExpression
    {
        private MethodInfo CurrentMethod { get; set; }
        private string RightValue { get; set; }
        private static Dictionary<string, MethodInfo> staticMethods;
        static readonly Regex nameRegex = new Regex("[a-zA-Z]+(?=\\s*=>)");
        static readonly Regex rightValueRegex = new Regex("(?<==>).+");
        static readonly Regex stringRegex = new Regex("(?<=\").*(?=\")");

        public StaticMethodModel()
        {
        }

        public StaticMethodModel(string command)
        {
            var methodName = GetMatch(nameRegex, command);

            if (staticMethods.ContainsKey(methodName))
                CurrentMethod = staticMethods[methodName];

            RightValue = GetMatch(rightValueRegex, command);
        }

        static StaticMethodModel()
        {
            staticMethods = new Dictionary<string, MethodInfo>
            {
                { "write", typeof(Console)
                           .GetMethod("WriteLine",
                                      new Type[] { typeof(string) })
                }
            };
        }

        private Expression GetExpression(string value)
        {
            var tmpString = GetMatch(stringRegex, value);

            if (tmpString != string.Empty)
                return Expression.Constant(tmpString);

            var expression = ExpressionTreeBuilder
                            .TransformToExpressionTree(value)
                            .Interpret();

            return Expression.Call(expression, typeof(double)
                                               .GetMethod("ToString",
                                                           new Type[] { }));
        }

        private static void AddStaticMethod(string name, MethodInfo method)
        {
            if (method == null) return;

            if (staticMethods.ContainsKey(name))
            {
                staticMethods[name] = method;
                return;
            }

            staticMethods.Add(name, method);
        }

        private string GetMatch(Regex regex, string input)
        {
            return regex.Match(input).Value;
        }

        public Expression Interpret()
        {
            var rigthExpression = GetExpression(RightValue);

            return Expression.Call(CurrentMethod, rigthExpression);
        }
    }
}
