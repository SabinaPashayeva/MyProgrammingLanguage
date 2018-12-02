using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public static class ExpressionTreeBuilder
    {
        private static Dictionary<Type, OperationPriority> keyValuePairs;
        private static ParameterExpression[] parameters;

        static ExpressionTreeBuilder()
        {
            keyValuePairs = new Dictionary<Type, OperationPriority>()
            {
                { typeof(PowerExpression), OperationPriority.PowerExpression },
                { typeof(MultiplyExpression), OperationPriority.MultiplyExpression },
                { typeof(DivideExpression), OperationPriority.DivideExpression },
                { typeof(AddExpression), OperationPriority.AddExpression },
                { typeof(SubtractExpression), OperationPriority.SubtractExpression }
            };
        }

        public static IExpression TransformToExpressionTree(string input)
        {
            Parser parser = new Parser(input);
            List<IExpression> initialExpressions = new List<IExpression>(parser.CreateObjectListFromFormula());
            parameters = new ParameterExpression[parser.Parameters.Count];
            parser.Parameters.CopyTo(parameters);

            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                for (int j = 0; j < initialExpressions.Count; j++)
                {
                    var expression = initialExpressions[j];

                    if (!(expression is TerminalExpression)) continue;
                    if (j == initialExpressions.Count - 1) break;
                    if (j == 0) continue;

                    int enumValue = (int)keyValuePairs[expression.GetType()];

                    if (enumValue == i)
                    {
                        TerminalExpression terminal = (TerminalExpression)expression;
                        terminal.leftExpression = initialExpressions[j - 1];
                        terminal.rightExpression = initialExpressions[j + 1];

                        initialExpressions[j] = (IExpression)terminal;
                        initialExpressions.RemoveAt(j - 1);
                        initialExpressions.RemoveAt(j);
                        j--;
                    }
                }
            }

            return initialExpressions.Count == 1 ? initialExpressions[0] : null;
        }

        public static T Build<T>(string formula)
        {
            var expression = TransformToExpressionTree(formula);

            try
            {
                var lambdaExpr = Expression.Lambda<T>(expression.Interpret(), parameters);

                return lambdaExpr.Compile();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return default(T);
            }
            catch (NullReferenceException nullEx)
            {
                Console.WriteLine(nullEx.Message);
                return default(T);
            }
        }
    }

    enum OperationPriority
    {
        PowerExpression,
        MultiplyExpression,
        DivideExpression,
        AddExpression,
        SubtractExpression
    }
}
