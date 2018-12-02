using System;

namespace IndividualTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            string formula = "12 + 43.2312 ^ x ^ y - 32 / 12";

            var result = ExpressionTreeBuilder.Build<Func<double, double, double>>(formula);

            if (result != null) Console.WriteLine(result(12, 10));
        }

    }
}
