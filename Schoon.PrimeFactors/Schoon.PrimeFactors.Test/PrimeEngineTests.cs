using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Schoon.PrimeFactors.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PrimeEngineTests
    {
        public PrimeEngineTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Factor_120()
        {
            PrimeEngine e = new PrimeEngine(int.MaxValue);

            IEnumerable<int> factors = e.GetPrimeFactors(120);

            string joined = String.Join(",", factors.Select(x => x.ToString()).ToArray());
            Assert.IsTrue(joined.Equals("2,2,2,3,5"));
        }

        [TestMethod]
        public void Factor_5()
        {
            PrimeEngine e = new PrimeEngine(int.MaxValue);

            IEnumerable<int> factors = e.GetPrimeFactors(5);

            string joined = String.Join(",", factors.Select(x => x.ToString()).ToArray());

            Assert.IsTrue(joined.Equals("5"));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Factor_TooLarge()
        {
            PrimeEngine e = new PrimeEngine(1000);

            IEnumerable<int> factors = e.GetPrimeFactors(5000);
        }
    }
}
