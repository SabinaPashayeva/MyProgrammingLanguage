using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public static class FileCompiler
    {
        public static void Compile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            var expressions = new List<Expression>();

            foreach (var line in lines)
            {
                var tmpExpression = Parser.GetExpression(line);
                expressions.Add(tmpExpression.Interpret());
            }

            var block = Expression.Block(VariableAssignModel.Parameters.Values,
                                         expressions);

            Expression.Lambda<Action>(block).Compile()();
        }
    }
}
