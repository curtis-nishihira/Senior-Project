using LongHorn.ArrowNav.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace LongHorn.ArrowNav.Tests
{
    [TestClass]
    public class FileArchiveServiceShould
    {
        [TestMethod]
        public void ArchiveValid()
        {
            //initialize
            ArchiveManager archiveManager = new ArchiveManager();
            var actual = archiveManager.Archive();
            var expected = "Archive Successful";

            Assert.IsTrue(actual == expected);
        }
    }
}