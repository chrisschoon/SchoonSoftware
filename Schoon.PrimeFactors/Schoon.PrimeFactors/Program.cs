using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;

namespace Schoon.PrimeFactors
{
    public sealed class Program
    {
        private static IPrimeEngine _engine;

        private Program()
        {

        }

        //this program accomidates integers up to some size limit.
        private const int MAXVALUE = int.MaxValue;

        public static void Main(string[] args)
        {
            _engine = new PrimeEngine(MAXVALUE);

            if (args.Length != 1)
            {
                Console.WriteLine(StringResources.Usage);
                return;
            }

            string filename = args[0];

            if (!File.Exists(filename))
            {
                Console.WriteLine(StringResources.FileNotFound, filename);
                return;
            }

            using (StreamReader tr = new StreamReader(filename))
            {
                //assuming this file is text with integers as strings
                String input;
                while ((input = tr.ReadLine()) != null)
                {
                    int value = 0;
                    if (!int.TryParse(input, out value))
                    {
                        Console.WriteLine(StringResources.Invalid);
                        continue;
                    }

                    if (value > MAXVALUE)
                    {
                        Console.WriteLine(StringResources.MaxValueExceeded, value, MAXVALUE);
                        continue;
                    }

                    //validation has passed
                    string result = GetPrimeFactorString(value);
                    Console.WriteLine(result);

                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Gets the prime factor string.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        private static string GetPrimeFactorString(int number)
        {
            string result = "";

            //get the prime factors
            IEnumerable<int> factors = _engine.GetPrimeFactors(number);

            //convert to a comma seperated string
            result = String.Join(StringResources.Delimiter, factors.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray());

            return result;
        }
    }
}