using System;

namespace IndividualTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            string formula = "1 + 10 - 5  + (3 - (2 * x)) * y";

            var result = ExpressionTreeBuilder.Build<Func<double, double, double>>(formula);

            if (result != null) Console.WriteLine(result(2, 1));
        }

    }
}
