using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    public static class Parser
    {
        private static readonly Dictionary<Regex, Func<string, IExpression>> typeList;
        private static readonly Regex staticMethodRegex = new Regex("[a-zA-Z].*=>.+");
        private static readonly Regex variableAssignRegex = new Regex("[a-zA-Z].*<=.+");

        static Parser()
        {
            typeList = new Dictionary<Regex, Func<string, IExpression>>
            {
                { staticMethodRegex, (str) => new StaticMethodModel(str) },
                { variableAssignRegex, (str) => new VariableAssignModel(str) }
            };
        }

        public static IExpression GetExpression(string command)
        {
            foreach (var pattern in typeList.Keys)
            {
                if (!pattern.IsMatch(command))
                    continue;

                return typeList[pattern](command);
            }

            return default(IExpression);
        }
    }
}
