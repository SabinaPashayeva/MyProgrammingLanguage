using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    public class Parser
    {
        private string _inputString;

        private Regex constantRegex = new Regex("\\b\\d+(\\.\\d*)?\\b");
        private Regex parameterRegex = new Regex("\\b[a-z,A-Z]\\w*\\b");
        private Regex plusRegex = new Regex("\\+");
        private Regex minusRegex = new Regex("\\-");
        private Regex multRegex = new Regex("\\*");
        private Regex divideRegex = new Regex("\\/");
        private Regex powerRegex = new Regex("\\^");

        private Dictionary<Regex, Func<IExpression>> typeList;
        public List<ParameterExpression> Parameters { get; private set; }

        public Parser(string input)
        {
            _inputString = input;

            typeList = new Dictionary<Regex, Func<IExpression>> {
                { constantRegex, () => { return new NumberExpression(); }},
                { plusRegex, () => { return new AddExpression(); }},
                { minusRegex, () => { return new SubtractExpression(); }},
                { multRegex, () => { return new MultiplyExpression(); }},
                { divideRegex, () => { return new DivideExpression(); }},
                { powerRegex, () => { return new PowerExpression(); }},
                { parameterRegex, () => { return new VariableExpression(); }}
            };

            Parameters = new List<ParameterExpression>();
        }

        public List<IExpression> CreateObjectListFromFormula()
        {
            Dictionary<int, IExpression> emptyObjectPosition = new Dictionary<int, IExpression>();

            foreach (Regex pattern in typeList.Keys)
            {
                foreach (Match match in pattern.Matches(_inputString))
                    emptyObjectPosition.Add(match.Index,
                                            CreateCorrespondingObject(pattern, match.Value));

            }

            var list = emptyObjectPosition.OrderBy(key => key.Key).Select(k => k.Value).ToList();

            return list;
        }

        private IExpression CreateCorrespondingObject(Regex pattern, string input)
        {
            IExpression expression = typeList[pattern]();

            if (expression is NumberExpression)
                ((NumberExpression)expression).Constant = Double.Parse(input);

            if (expression is VariableExpression)
            {
                var param = Expression.Parameter(typeof(double));
                Parameters.Add(param);
                ((VariableExpression)expression).Parameter = param;
            }

            return expression;
        }

    }
}
