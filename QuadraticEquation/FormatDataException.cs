using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadraticEquation
{
    public class FormatDataException : Exception
    {
        public Severity Severity;
        public FormatDataException(string message) : base (message) { }
    }

}
