using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Schoon.PrimeFactors.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ProgramTests
    {
        public ProgramTests()
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
        public void ProgramTests_BadPath()
        {
            string badpath = @"badcharacters'in+here.txt";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                
                Program.Main(new [] { badpath });

                string expected = string.Format(StringResources.FileNotFound + "{1}", badpath, Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void ProgramTests_FileNotFound()
        {
            string badpath = @"nofile.txt";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Program.Main(new[] { badpath });

                string expected = string.Format(StringResources.FileNotFound + "{1}", badpath, Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }

        [TestMethod]
        [DeploymentItem(@"TestFiles\CleanFile.txt")]
        public void ProgramTests_CleanSmallFile()
        {
            string inputfile = @"CleanFile.txt";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Program.Main(new[] { inputfile });

                string output = sw.ToString();

                Assert.IsFalse(output.Contains(StringResources.Invalid));
            }
        }

        [TestMethod]
        [DeploymentItem(@"TestFiles\DirtyFile.txt")]
        public void ProgramTests_DirtySmallFile()
        {
            string inputfile = @"DirtyFile.txt";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Program.Main(new[] { inputfile });

                string output = sw.ToString();

                Assert.IsTrue(output.Contains(StringResources.Invalid));
            }
        }

        [TestMethod]
        [DeploymentItem(@"TestFiles\20x4.txt")]
        public void ProgramTests_20x4()
        {
            //small enough file to not take too long
            string inputfile = @"20x4.txt";
            List<int> inputLines = new List<int>();

            //read the file in
            using (StreamReader tr = new StreamReader(inputfile))
            {
                String input;
                while ((input = tr.ReadLine()) != null)
                {
                    int value = 0;
                    if (int.TryParse(input, out value))
                    {
                        inputLines.Add(value);
                    }
                }
            }

            string[] outputLines = null;
            
            //catch the console output
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Program.Main(new[] { inputfile });

                //TODO - look into a more straightforward way
                outputLines = sw.ToString().Trim().Split('\n');
            }

            //should have same amount of output as input
            Assert.IsTrue(outputLines.Length == inputLines.Count);

            for (int i = 0; i < inputLines.Count; i++)
            {
                //multiple the output to see if it matches the input
                string[] factors = outputLines[i].Trim().Split(StringResources.Delimiter.ToCharArray());
                int[] nFactors = factors.Select(x => System.Convert.ToInt32(x)).ToArray();
                int result = nFactors.Aggregate((x1 , x2) => x1 * x2);
                Assert.IsTrue(inputLines[i] == result);
            }
        }
    }
}
