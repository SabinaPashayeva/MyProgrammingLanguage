using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    public interface IExpression
    {
        Expression Interpret(Context context);
    }
}
