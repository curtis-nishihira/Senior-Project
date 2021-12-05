using Microsoft.VisualStudio.TestTools.UnitTesting;
using LongHorn.Archiving;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class FileArchiveServiceShould
    {
        [TestMethod]
        public void TestMethod1()
        {
            //initialize
            IArchiveService service = new FileArchiveService();

            ////execute
            //var actualOutput = service.Log("Log entry number 1");
            //var expectedOutput = true;
            //var actualTimeElapsed = service.getElapsedTime();
            //var expectedTime = 5;

            ////comparision
            //Assert.IsTrue(actualOutput == expectedOutput);
            //Assert.IsTrue(actualTimeElapsed <= expectedTime);
        }
    }
}
