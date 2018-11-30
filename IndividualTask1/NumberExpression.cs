using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public class NumberExpression : IExpression
    {
        string name; // имя переменной
        int constant;

        public NumberExpression(string variableName)
        {
            name = variableName;
        }

        public NumberExpression(int constant)
        {
            this.constant = constant;
        }

        public Expression Interpret(Context context)
        {
            if (name != null) return context.GetVariable(name);

            return Expression.Constant(constant);
        }
    }
}
