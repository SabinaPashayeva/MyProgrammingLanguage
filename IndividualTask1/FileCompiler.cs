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

                if (TryCompileLine(tmpExpression, out Expression compiledExpression))
                    expressions.Add(compiledExpression);
            }

            var block = Expression.Block(VariableAssignModel.Parameters.Values,
                                         expressions);

            Expression.Lambda<Action>(block).Compile()();
        }

        private static bool TryCompileLine(IExpression expression,
                                        out Expression compiledExpression)
        {
            compiledExpression = null;

            try
            {
                compiledExpression = expression.Interpret();
                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
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
