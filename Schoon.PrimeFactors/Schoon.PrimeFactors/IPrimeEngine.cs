using System;

namespace Schoon.PrimeFactors
{
    /// <summary>
    /// The contract for a Prime Engine
    /// </summary>
    public interface IPrimeEngine
    {
        System.Collections.Generic.IEnumerable<int> GetPrimeFactors(int value);
        System.Collections.Generic.IEnumerable<int> Primes();
    }
}
