using System;
using System.Collections;
using System.Collections.Generic;

namespace QuadraticEquation
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<int> values = GetInputValues();
                CountingResult(values[0], values[1], values[2]);
            }
            catch (FormatDataException ex)
            {
                FormatData(ex.Message, ex.Severity, ex.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static List<int> GetInputValues()
        {
            List<int> intArr = new();
            bool beacon = false;

            Console.WriteLine($"a * x ^ 2 + b * x + c = 0");

            do
            {
                Console.WriteLine($"Input value a: ");
                string? a1 = Console.ReadLine();
                Console.WriteLine($"Input value b: ");
                string? b1 = Console.ReadLine();
                Console.WriteLine($"Input value c: ");
                string? c1 = Console.ReadLine();

                List<string> wrongValues = new();

                bool tryParseA = int.TryParse(a1, out int a);
                bool tryParseB = int.TryParse(b1, out int b);
                bool tryParseC = int.TryParse(c1, out int c);

                if (tryParseA) intArr.Add(a);
                else wrongValues.Add("a");

                if (tryParseB) intArr.Add(b);
                else wrongValues.Add("b");

                if (tryParseC) intArr.Add(c);
                else wrongValues.Add("c");

                if (!tryParseA || !tryParseB || !tryParseC)
                {
                    string massage = $"Invalid parameter format {string.Join(",", wrongValues)}";
                    FormatDataException ex = new FormatDataException(massage);
                    ex.Data.Add("a", a1);
                    ex.Data.Add("b", b1);
                    ex.Data.Add("c", c1);
                    ex.Severity = Severity.Error;
                    throw ex;
                }

                beacon = true;

            } while (!beacon);

            return intArr;
        }

        public static void CountingResult(int a, int b, int c)
        {
            double discriminant = Math.Pow(b, 2) - (4 * a * c);

            if (discriminant < 0)
            {
                FormatDataException error = new FormatDataException("No real values found!");
                error.Severity = Severity.Warning;
                throw error;
            }
            else
            {
                double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

                if (discriminant > 0) Console.WriteLine($"x1 = {x1}, x2 = {x2}");
                else Console.WriteLine($"x = {x1}");
            }
        }

        public static void FormatData(string message, Severity severity, IDictionary data)
        {
            string restrictions = string.Join("", Enumerable.Repeat("-", 50));
            string outputMassage = $"{restrictions} \n {message} \n {restrictions}";
            string infoMassage = $"{restrictions} \n As a value you may enter only integers," +
                $"\n avaliable range [-2 147 483 648, 2 147 483 647] \n {restrictions}";

            if (severity == Severity.Error)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(outputMassage);
                Console.WriteLine($"a = {data["a"]}");
                Console.WriteLine($"b = {data["b"]}");
                Console.WriteLine($"c = {data["c"]}");
                Console.ResetColor();

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(infoMassage);
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(outputMassage);
                Console.ResetColor();
            }
        }
    }
}
