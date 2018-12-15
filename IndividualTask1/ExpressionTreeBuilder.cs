using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public static class ExpressionTreeBuilder
    {
        private static Dictionary<Type, OperationPriority> priorities;

        static ExpressionTreeBuilder()
        {
            priorities = new Dictionary<Type, OperationPriority>()
            {
                { typeof(PowerModel), OperationPriority.PowerExpression },
                { typeof(MultiplyModel), OperationPriority.MultiplyExpression },
                { typeof(DivideModel), OperationPriority.DivideExpression },
                { typeof(AddModel), OperationPriority.AddExpression },
                { typeof(SubtractModel), OperationPriority.SubtractExpression }
            };
        }

        public static IExpression TransformToExpressionTree(string input)
        {
            ArithmeticalParser parser = new ArithmeticalParser(input);
            var initialExpressions = new List<IExpression>(parser.CreateObjectListFromFormula());
            CreateParameterList(initialExpressions);

            for (int i = 0; i < priorities.Count; i++)
            {
                for (int j = 0; j < initialExpressions.Count; j++)
                {
                    var expression = initialExpressions[j];

                    if (!(expression is NonTerminalExpression)) continue;
                    if (j == initialExpressions.Count - 1) break;
                    if (j == 0) continue;

                    int enumValue = (int)priorities[expression.GetType()];

                    if (enumValue / 10 == i)
                    {
                        var nonTerminal = (NonTerminalExpression)expression;
                        nonTerminal.LeftExpression = initialExpressions[j - 1];
                        nonTerminal.RightExpression = initialExpressions[j + 1];

                        initialExpressions[j] = (IExpression)nonTerminal;
                        initialExpressions.RemoveAt(j - 1);
                        initialExpressions.RemoveAt(j);
                        j--;
                    }
                }
            }

            return initialExpressions.Count == 1 ? initialExpressions[0] : null;
        }

        private static void CreateParameterList(List<IExpression> initialExpressions)
        {
            foreach (IExpression expression in initialExpressions)
            {
                if (expression is ParameterModel)
                {
                    var variableName = ((ParameterModel)expression).ParameterName;
                    var paramDict = VariableAssignModel.Parameters;

                    if (paramDict.ContainsKey(variableName))
                        ((ParameterModel)expression).Parameter = paramDict[variableName];
                }
            }
        }

        //public static T Build<T>(string formula)
        //{
        //    var expression = TransformToExpressionTree(formula);

        //    try
        //    {
        //        var lambdaExpr = Expression.Lambda<T>(expression.Interpret(), parameters);

        //        return lambdaExpr.Compile();
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return default(T);
        //    }
        //    catch (NullReferenceException nullEx)
        //    {
        //        Console.WriteLine(nullEx.Message);
        //        return default(T);
        //    }
        //}
    }

    enum OperationPriority
    {
        PowerExpression = 0,
        MultiplyExpression = 10,
        DivideExpression,
        AddExpression = 20,
        SubtractExpression
    }
}
