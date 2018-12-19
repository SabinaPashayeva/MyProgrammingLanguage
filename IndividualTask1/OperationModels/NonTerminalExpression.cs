using System;

namespace IndividualTask1
{
    public abstract class NonTerminalExpression
    {
        public IExpression LeftExpression { get; private set; }
        public IExpression RightExpression { get; private set; }

        protected NonTerminalExpression()
        {
        }

        public void SetExpressions(IExpression left, IExpression right)
        {
            if (left != null && right != null)
            {
                LeftExpression = left;
                RightExpression = right;
            }
        }

        protected NonTerminalExpression(IExpression left, IExpression right)
        {
            LeftExpression = left;
            RightExpression = right;
        }
    }
}
