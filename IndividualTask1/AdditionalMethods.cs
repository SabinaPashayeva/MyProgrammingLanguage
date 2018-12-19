using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace IndividualTask1
{
    public class AdditionalMethods : IEnumerable<string>
    {
        private static Dictionary<string, MethodInfo> methods;

        public AdditionalMethods()
        {
            methods = new Dictionary<string, MethodInfo>
            {
                { "calculate", typeof(AdditionalMethods).GetMethod("Calculate")},
                { "factorial", typeof(AdditionalMethods).GetMethod("Factorial")},
                { "convert", typeof(AdditionalMethods).GetMethod("ConvertToBase")},
                { "write", typeof(Console)
                           .GetMethod("WriteLine",
                                      new Type[] { typeof(string) })
                }
            };
        }

        public IEnumerator<string> GetEnumerator()
        {
            return methods.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public MethodInfo this[string key]
        {
            get
            {
                return methods[key];
            }
        }

        public bool ContainsKey(string key)
        {
            return methods.ContainsKey(key);
        }

        public static void AddMethod(string name, MethodInfo method)
        {
            if (methods.ContainsKey(name))
            {
                methods[name] = method;
                return;
            }

            methods.Add(name, method);
        }

        public static void ConvertToBase(string input)
        {
            if (!int.TryParse(input, out int number))
            {
                Console.WriteLine("Wrong input format!");
                return;
            }

            Console.WriteLine("Base: ");
            string baseStr = Console.ReadLine();
            var possibleBases = new List<int> { 2, 8, 10, 16 };

            if (!int.TryParse(baseStr, out int baseInt) &&
                 !possibleBases.Contains(baseInt))
            {
                Console.WriteLine("Wrong input format!");
                return;
            }

            Console.WriteLine(Convert.ToString(number, baseInt));
        }

        public static void Calculate(string input)
        {
            Console.WriteLine("Enter arithmetical expression: ");
            string formula = Console.ReadLine();
            try
            {
                var expression = ExpressionTreeBuilder.TransformToExpressionTree(formula);
                var lambda = Expression.Lambda<Func<double>>(expression.Interpret());

                Console.WriteLine(lambda.Compile()());
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Wrong input format");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Factorial(string input)
        {
            if (!int.TryParse(input, out int number))
            {
                Console.WriteLine("Wrong input format");
                return;
            }

            Console.WriteLine($"({number})! = {RecursiveFactorial(number)}");
        }

        private static double RecursiveFactorial(int number)
        {
            if (number == 1)
                return 1;

            return number * RecursiveFactorial(number - 1);
        }
    }
}
