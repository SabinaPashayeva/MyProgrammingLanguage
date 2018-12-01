using System;
using System.Collections.Generic;

namespace IndividualTask1
{
    public static class ExpressionTreeBuilder
    {
        private static Dictionary<Type, OperationPriority> keyValuePairs;

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
            List<IExpression> initialExpressions = new List<IExpression>(parser.CreateObjectList());

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
            if (initialExpressions.Count == 1)
                return initialExpressions[0];

            return null;
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
