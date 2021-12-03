using NUnit.Framework;
using TestLibrary;

namespace NUnitTestBenchmark
{
    public class Tests
    {
        private Test test;
        [SetUp]
        public void Setup()
        {
            test = new Test();
        }

        [Test]
        public void Test1()
        {
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