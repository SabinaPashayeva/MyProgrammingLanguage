using System;
namespace IndividualTask1
{
    public abstract class TerminalExpression
    {
        public IExpression leftExpression;
        public IExpression rightExpression;

        protected TerminalExpression()
        {
        }

        protected TerminalExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }
    }
}
