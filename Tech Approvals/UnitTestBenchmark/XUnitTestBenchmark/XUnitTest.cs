using System;
using TestLibrary;
using Xunit;

namespace XUnitTestBenchmark
{
    public class XUnitTest
    {
        [Fact]
        public void Test1()
        {
            Test test = new Test();
            var actualOutput = test.generateList();
            var expectedOutput = true;

            var actualTimeElapsed = test.getElapsedTime();
            var expectedTime = 5;

            //comparision
            Assert.True(actualOutput == expectedOutput);
            Assert.True(actualTimeElapsed <= expectedTime);
        }
    }
}
