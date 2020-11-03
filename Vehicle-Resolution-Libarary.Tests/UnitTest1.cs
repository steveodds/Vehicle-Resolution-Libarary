using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vehicle_Resolution_Libarary.Tests
{
    [TestClass]
    public class UnitTest1
    {

        //private string GetResult(string input)
        //{
        //    var lib = new Start();
        //    var result = lib.ResolveSingleWord(input);

        //    return result;
        //}

        [TestMethod]
        public void TestNull()
        {
            string input = null;
            var lib = new Start();
            Assert.AreEqual(lib.ResolveSingleWord(input, input), "ERROR: The argument was empty.");
        }

        [TestMethod]
        public void TestWrongRefFileInfo()
        {
            var lib = new Start();
            string input = "SUNARU";
            string refile = @"C:\test.xlsx";
            Assert.AreEqual(lib.ResolveSingleWord(input, refile), "ERROR: Could not find file");
        }

        [TestMethod]
        public void TestSampleWord()
        {
            var lib = new Start();
            //var test = lib.ResolveSingleWord("SUNARU");
            Assert.AreEqual(lib.ResolveSingleWord("SUNARU", @"C:\Users\user\Documents\sample\Agilis LIVE Make & Models.csv"), "SUBARU");
        }

    }
}
