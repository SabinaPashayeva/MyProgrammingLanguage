using System;
namespace IndividualTask1
{
    public abstract class NonTerminalExpression
    {
        public IExpression LeftExpression { get; set; }
        public IExpression RightExpression { get; set; }

        protected NonTerminalExpression()
        {
        }

        protected NonTerminalExpression(IExpression left, IExpression right)
        {
            LeftExpression = left;
            RightExpression = right;
        }
    }
}
