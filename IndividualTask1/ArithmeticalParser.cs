using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    public class ArithmeticalParser
    {
        private string _inputString;

        static readonly Regex constantRegex = new Regex("\\b\\d+(\\.\\d*)?\\b");
        static readonly Regex parameterRegex = new Regex("\\b[a-z,A-Z]\\w*\\b");
        static readonly Regex plusRegex = new Regex("\\+");
        static readonly Regex minusRegex = new Regex("\\-");
        static readonly Regex multRegex = new Regex("\\*");
        static readonly Regex divideRegex = new Regex("\\/");
        static readonly Regex powerRegex = new Regex("\\^");
        static readonly Regex parenthesesRegex = new Regex("(?<=\\()[^\\)]+(\\(.+" +
                                                           "\\))*[^\\(]*(?=\\))");


        readonly Dictionary<Regex, Func<IExpression>> typeList;
        readonly Dictionary<Type, Func<string, IExpression>> terminalExpressions;

        public ArithmeticalParser(string input)
        {
            _inputString = input;

            typeList = new Dictionary<Regex, Func<IExpression>>
            {
                { parenthesesRegex, () => { return new ParenthesesModel(); }},
                { constantRegex, () => { return new NumberModel(); }},
                { plusRegex, () => { return new AddModel(); }},
                { minusRegex, () => { return new SubtractModel(); }},
                { multRegex, () => { return new MultiplyModel(); }},
                { divideRegex, () => { return new DivideModel(); }},
                { powerRegex, () => { return new PowerModel(); }},
                { parameterRegex, () => { return new ParameterModel(); }}
            };

            terminalExpressions = new Dictionary<Type, Func<string, IExpression>>
            {
                { typeof(NumberModel),
                    (str) => { return new NumberModel(double.Parse(str)); }},
                { typeof(ParenthesesModel),
                    (str) => { return new ParenthesesModel(str); }},
                { typeof(ParameterModel),
                    (str) => { return new ParameterModel(str);  }}
            };
        }

        public List<IExpression> CreateObjectListFromFormula()
        {
            Dictionary<int, IExpression> emptyObjectPosition =
                new Dictionary<int, IExpression>();

            foreach (Regex pattern in typeList.Keys)
            {
                foreach (Match match in pattern.Matches(_inputString))
                {
                    emptyObjectPosition.Add(match.Index,
                                                CreateCorrespondingObject(pattern,
                                                                          match.Value));
                    if (pattern == parenthesesRegex)
                        ChangeCharacters(ref _inputString, match.Index,
                                 match.Value.Length);
                }
            }

            var list = emptyObjectPosition
                .OrderBy(key => key.Key)
                .Select(k => k.Value)
                .ToList();

            return list;
        }

        private IExpression CreateCorrespondingObject(Regex pattern, string input)
        {
            var expression = typeList[pattern]();

            foreach (var type in terminalExpressions.Keys)
            {
                if (expression.GetType() != type)
                    continue;

                return terminalExpressions[type](input);
            }

            return expression;
        }

        private void ChangeCharacters(ref string input, int start, int length)
        {
            char[] result = input.ToCharArray();

            for (int i = start - 1; i <= start + length; i++)
            {
                result[i] = ' ';
            }

            input = new string(result);
        }

        private IExpression GetNestedExpression(string input)
        {
            return ExpressionTreeBuilder.TransformToExpressionTree(input);
        }
    }
}
