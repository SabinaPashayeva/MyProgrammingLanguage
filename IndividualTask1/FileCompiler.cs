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

                if (TryInterpet(tmpExpression))
                    expressions.Add(tmpExpression.Interpret());
            }

            var block = Expression.Block(VariableAssignModel.Parameters.Values,
                                         expressions);

            Expression.Lambda<Action>(block).Compile()();
        }

        private static bool TryInterpet(IExpression expression)
        {
            try
            {
                expression.Interpret();
                return true;
            }
            catch (ArgumentException ax)
            {
                Console.WriteLine(ax.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
