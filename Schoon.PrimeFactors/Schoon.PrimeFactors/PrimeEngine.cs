using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Globalization;

namespace Schoon.PrimeFactors
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimeEngine : Schoon.PrimeFactors.IPrimeEngine
    {
        //TODO - cache known primes to disk for reuse accross program runs
        private List<int> _primes = new List<int>();
        private int _lastChecked;
        private int _MaxValue = 0;

        public PrimeEngine(int maxValue)
        {
            _primes.Add(2);
            _lastChecked = 2;
            _MaxValue = maxValue;
        }

        /// <summary>
        /// Determines whether the specified canidate value is prime.
        /// </summary>
        /// <param name="canidateValue">The canidate value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified canidate value is prime; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPrime(int canidateValue)
        {
            bool isPrime = true;

            foreach (int prime in _primes)
            {
                if ((canidateValue % prime) == 0 && prime <= Math.Sqrt(canidateValue))
                {
                    isPrime = false;
                    break;
                }
            }

            return isPrime;
        }

        /// <summary>
        /// Provides the ordered sequence of all known primes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> Primes()
        {
            //start with primes we have already
            foreach (int prime in _primes)
            {
                yield return prime;
            }

            //move into 
            while (_lastChecked < _MaxValue)
            {
                _lastChecked++;

                if (IsPrime(_lastChecked))
                {
                    _primes.Add(_lastChecked);
                    yield return _lastChecked;
                }
            }
        }

        /// <summary>
        /// Gets the prime factors of a value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IEnumerable<int> GetPrimeFactors(int value)
        {
            if (value > _MaxValue)
            {
                throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, StringResources.MaxValueExceeded, value, _MaxValue));
            }

            List<int> factors = new List<int>();

            foreach (int prime in Primes())
            {
                while (value % prime == 0)
                {
                    value /= prime;
                    factors.Add(prime);
                }

                if (value == 1)
                {
                    break;
                }
            }

            return factors;
        }
    }
}
