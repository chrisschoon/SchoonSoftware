using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schoon.PrimeFactors
{
    /// <summary>
    /// The Messages class contains string resources in english
    /// </summary>
    public sealed class StringResources
    {
        public const string FileNotFound = "The specified filename '{0}' doesn't exist.";
        public const string Usage = "Usage: Schoon.PrimeFactors.exe <filename> .";
        public const string Delimiter = ",";
        public const string Invalid = "invalid";
        public const string MaxValueExceeded = "Value {0} exceeeds the maximum of {1}";

        private StringResources()
        {

        }
    }
}
