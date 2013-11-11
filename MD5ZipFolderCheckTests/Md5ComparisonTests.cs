using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD5ZipCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Compression;
using System.IO;
namespace MD5ZipCheck.Tests
{
    [TestClass()]
    public class Md5ComparisonTests
    {
        private string TempPath { get; set; }
        private string ZipFileName { get; set; }
        private string SubdirectoryName { get; set; }

        [TestInitialize]
        public void InitializeTest()
        {
            //create a ZIP file in the tmp folder
            TempPath = Path.GetTempPath();
            ZipFileName = "test.zip";
            SubdirectoryName = "test";

            //create test directory and test files
            var subdirectoryPath = Path.Combine(TempPath, SubdirectoryName);
            Directory.CreateDirectory(subdirectoryPath);

            var random = new Random(System.Environment.TickCount);
            var buffer = new byte[4096];
            for (int i = 0; i < 5; i++)
            {
                var file = File.Create(Path.Combine(subdirectoryPath, "test" + i + ".tst"));
                random.NextBytes(buffer);
                file.Write(buffer, 0, buffer.Length);
                file.Close();
            }

            Directory.CreateDirectory(Path.Combine(subdirectoryPath, "subdir"));
            for (int i = 0; i < 5; i++)
            {
                var file = File.Create(Path.Combine(subdirectoryPath, "subdir", "test" + i + ".tst"));
                random.NextBytes(buffer);
                file.Write(buffer, 0, buffer.Length);
                file.Close();
            }

            //create an example ZIP file
            ZipFile.CreateFromDirectory(subdirectoryPath, Path.Combine(TempPath, ZipFileName));
        }

        [TestCleanup]
        public void CleanUpTest()
        {
            Directory.Delete(Path.Combine(TempPath, SubdirectoryName), true);
            File.Delete(Path.Combine(TempPath, ZipFileName));
        }
        
        [TestMethod()]
        public void Md5ComparisonTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Md5ComparisonTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Md5ComparisonTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Md5ComparisonTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CompareTest()
        {
            Assert.Fail();
        }
    }
}
