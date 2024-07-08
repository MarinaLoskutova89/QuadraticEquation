using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QuadraticEquation
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<int> values = Logics.GetInputValues();
                Logics.CountingResult(values[0], values[1], values[2]);
            }
            catch (FormatDataException ex)
            {
                Logics.FormatData(ex.Message, ex.Severity, ex.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
