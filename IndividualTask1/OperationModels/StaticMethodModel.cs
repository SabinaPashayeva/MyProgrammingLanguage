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
        private static Dictionary<string, MethodInfo> staticMethods;

        public StaticMethodModel()
        {
        }

        public StaticMethodModel(string command)
        {
            var methodName = nameRegex.Match(command).Value;

            if (staticMethods.ContainsKey(methodName))
                CurrentMethod = staticMethods[methodName];

            RightValue = rightValueRegex.Match(command).Value;
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

        public Expression Interpret()
        {
            var rigthValueParser = new RightStatementParser(RightValue, typeof(string));

            return Expression.Call(CurrentMethod, rigthValueParser.GetRightExpression());
        }
    }
}
