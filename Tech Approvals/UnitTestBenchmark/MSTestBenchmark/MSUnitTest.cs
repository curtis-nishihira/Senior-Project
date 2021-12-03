using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestLibrary;

namespace MSTestBenchmark
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Test test = new Test();
            var actualOutput = test.generateList();
            var expectedOutput = true;

            var actualTimeElapsed = test.getElapsedTime();
            var expectedTime = 5;

            //comparision
            Assert.IsTrue(actualOutput == expectedOutput);
            Assert.IsTrue(actualTimeElapsed <= expectedTime);
        }
    }
}
