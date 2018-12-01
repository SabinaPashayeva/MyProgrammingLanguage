using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    public class Parser
    {
        private string _inputString;

        private Regex constantRegex = new Regex("\\d+(\\.\\d*)?");
        private Regex plusRegex = new Regex("\\+");
        private Regex minusRegex = new Regex("\\-");
        private Regex multRegex = new Regex("\\*");
        private Regex divideRegex = new Regex("\\/");
        private Regex powerRegex = new Regex("\\^");

        private Dictionary<Regex, Func<IExpression>> typeList;
        private List<string> tokenList;

        public Parser(string input)
        {
            _inputString = input;

            typeList = new Dictionary<Regex, Func<IExpression>> {
                { constantRegex, () => { return new NumberExpression(); }},
                { plusRegex, () => { return new AddExpression(); }},
                { minusRegex, () => { return new SubtractExpression(); }},
                { multRegex, () => { return new MultiplyExpression(); }},
                { divideRegex, () => { return new DivideExpression(); }},
                { powerRegex, () => { return new PowerExpression(); }}
            };
        }

        public List<IExpression> CreateObjectList() 
        {
            GetTokenList();
            List<IExpression> emptyObjectList = new List<IExpression>();

            foreach (string token in tokenList)
            {
                foreach (Regex pattern in typeList.Keys)
                {
                    if (pattern.IsMatch(token))
                    {
                        IExpression expression = typeList[pattern]();

                        if (expression is NumberExpression)
                            ((NumberExpression)expression).Constant = Double.Parse(token);
                       
                        emptyObjectList.Add(expression);
                        
                        break;
                    }
                }
            }
            return emptyObjectList;
        }

        private void GetTokenList() 
        {
            tokenList = new List<string>();
            bool isPreviousDigit = false;
            int start = 0;
            int count = 0;

            for (int i = 0; i < _inputString.Length; i++)
            {
                char currentChar = _inputString[i];
                if (!isPreviousDigit && Char.IsDigit(currentChar))
                {
                    start = i;
                    count++;
                    isPreviousDigit = true;

                    if (i == _inputString.Length - 1) tokenList.Add(_inputString.Substring(start));
                    continue;
                }
                if (isPreviousDigit && (Char.IsDigit(currentChar) || currentChar == '.') )
                {
                    count++;
                    isPreviousDigit = true;

                    if (i == _inputString.Length - 1) tokenList.Add(_inputString.Substring(start));
                    continue;
                }
                if (isPreviousDigit && !Char.IsDigit(currentChar) && currentChar != '.') 
                {
                    tokenList.Add(_inputString.Substring(start, count));
                }
                if (!Char.IsWhiteSpace(currentChar))
                {
                    tokenList.Add(currentChar.ToString());
                }
                isPreviousDigit = false;
                start = 0;
                count = 0;
            }
        }

    }
}
