using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace IndividualTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "12 + 43.2312 ^ 1 ^ 3 - 32 / 12";

            var expression = ExpressionTreeBuilder.TransformToExpressionTree(a);

            double result = Expression.Lambda<Func<double>>(expression.Interpret()).Compile()();

            Console.WriteLine(result);
        }
    }
}
