using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vehicle_Resolution_Libarary.Tests
{
    [TestClass]
    public class UnitTest1
    {

        private string GetResult(string input)
        {
            var lib = new Start();
            var result = lib.ResolveSingleWord(input);

            return result;
        }

        [TestMethod]
        public void TestNull()
        {
            string input = null;
            var lib = new Start();
            Assert.ThrowsException<ArgumentException>(()=> lib.ResolveSingleWord(input));
        }

        [TestMethod]
        public void TestSampleWord()
        {
            var lib = new Start();
            //var test = lib.ResolveSingleWord("SUNARU");
            Assert.AreEqual(lib.ResolveSingleWord("SUNARU"), "SUBARU");
        }

    }
}
