using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class ParenthesesModel : IExpression
    {
        private readonly string _command;

        public ParenthesesModel()
        {
        }

        public ParenthesesModel(string command)
        {
            _command = command;
        }

        public Expression Interpret()
        {
            return ExpressionTreeBuilder.TransformToExpressionTree(_command)
                                        .Interpret();
        }
    }
}
