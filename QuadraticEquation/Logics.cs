using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadraticEquation
{
    public class Logics
    {
        public static int columnPosition = 0;
        public static int rowPosition = 0;
        public static Dictionary<string, string> result = new Dictionary<string, string>();

        public static List<int> GetInputValues()
        {
            List<int> intArr = new();
            bool beacon = false;
            ConsoleKeyInfo ki;
            var sb = new StringBuilder();
            string wrongValues = "";

            rowPosition = 1;

            PrintMenu();
            WriteCursor(columnPosition, rowPosition);
            do
            {
                ki = Console.ReadKey();
                if (ki.Key == ConsoleKey.UpArrow || ki.Key == ConsoleKey.DownArrow || ki.Key == ConsoleKey.Enter) sb.Clear();
                ClearCursor(columnPosition, rowPosition);

                switch (ki.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (rowPosition > 1) rowPosition--;
                        else rowPosition = 3;
                        break;

                    case ConsoleKey.DownArrow:
                        if (rowPosition < options.Length) rowPosition++;
                        else rowPosition = 1;
                        break;

                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(columnPosition, 4);
                        break;

                    default:
                        var sbLength = sb.Length;
                        columnPosition = 3 + sbLength;
                        Console.SetCursorPosition(columnPosition, rowPosition);
                        var kiString = ki.KeyChar.ToString();
                        sb.Append(kiString);
                        Console.SetCursorPosition(columnPosition, rowPosition);
                        ResultDict(sb.ToString(), rowPosition);
                        Console.Clear();
                        PrintMenu();
                        columnPosition = 0;
                        break;
                }

                if (ki.Key != ConsoleKey.Enter)
                {
                    WriteCursor(columnPosition, rowPosition);
                }
                else
                {
                    string? a1 = result["a"];
                    string? b1 = result["b"];
                    string? c1 = result["c"];

                    Exception ex = new Exception();
                    ex.Data.Add("a", a1);
                    ex.Data.Add("b", b1);
                    ex.Data.Add("c", c1);

                    try
                    {
                        wrongValues = "a";
                        int a = int.Parse(a1);
                        intArr.Add(a);

                        wrongValues = "b";
                        int b = int.Parse(b1);
                        intArr.Add(b);

                        wrongValues = "c";
                        int c = int.Parse(c1);
                        intArr.Add(c);

                        beacon = true;
                    }
                    catch (OverflowException)
                    {
                        FormatData($"Invalid parameter format {wrongValues}", Severity.Information, ex.Data);
                        rowPosition = 1;
                        WriteCursor(columnPosition, rowPosition);
                    }
                    catch (Exception)
                    {
                        FormatData($"Invalid parameter format {wrongValues}", Severity.Error, ex.Data);
                        rowPosition = 1;
                        WriteCursor(columnPosition, rowPosition);
                    }
                }
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
            }

            else if (severity == Severity.Information)
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

        public static string[] options = new[]
        {
            "a:",
            "b:",
            "c:",
        };
        public static void WriteCursor(int column, int row)
        {
            Console.SetCursorPosition(column, row);
            Console.Write(">");
            column += 3;
            Console.SetCursorPosition(column, row);
        }

        public static void ClearCursor(int column, int row)
        {
            Console.SetCursorPosition(column, row);
            Console.Write(" ");
            Console.SetCursorPosition(column, row);
        }

        public static void PrintMenu()
        {
            string a = !result.ContainsKey("a") ? "a" : result["a"];
            string b = !result.ContainsKey("b") ? "b" : result["b"];
            string c = !result.ContainsKey("c") ? "c" : result["c"];

            string aa = !result.ContainsKey("a") ? string.Empty : result["a"];
            string bb = !result.ContainsKey("b") ? string.Empty : result["b"];
            string cc = !result.ContainsKey("c") ? string.Empty : result["c"];

            if (b.StartsWith('-') && c.StartsWith('-')) Console.WriteLine($"{a} * x ^ 2 - {b.Replace("-", "")} * x - {c} = 0");
            else if (b.StartsWith('-')) Console.WriteLine($"{a} * x ^ 2 - {b.Replace("-", "")} * x + {c.Replace("-", "")} = 0");
            else if (c.StartsWith('-')) Console.WriteLine($"{a} * x ^ 2 + {b.Replace("-", "")} * x - {c.Replace("-", "")} = 0");
            else Console.WriteLine($"{a} * x ^ 2 + {b} * x + {c} = 0");

            Console.WriteLine($" {options[0]}{aa}");
            Console.WriteLine($" {options[1]}{bb}");
            Console.WriteLine($" {options[2]}{cc}");
        }
        public static Dictionary<string, string> ResultDict(string input, int row)
        {
            if (row == 1)
            {
                if (result.ContainsKey("a")) result["a"] = input;
                else result.Add("a", input);
            }
            else if (row == 2)
            {
                if (result.ContainsKey("b")) result["b"] = input;
                else result.Add("b", input);
            }
            else
            {
                if (result.ContainsKey("c")) result["c"] = input;
                else result.Add("c", input);
            }
            return result;
        }
    }
}
