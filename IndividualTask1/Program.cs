using System;
using System.Linq.Expressions;

namespace IndividualTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();

            context.SetVariable("a", 12);
            context.SetVariable("b", 10);

            IExpression expression = new DivideExpression(
                new MultiplyExpression(
                    new AddExpression(
                        new NumberExpression("a"),
                        new NumberExpression("b")
                    ), new NumberExpression(10)
                ), new NumberExpression(2));

            int result = Expression.Lambda<Func<int>>(expression.Interpret(context)).Compile()();

            Console.WriteLine(result);
        }
    }
}
