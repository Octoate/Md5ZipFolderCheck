using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD5ZipCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;
namespace MD5ZipCheck.Tests
{
    [TestClass()]
    public class Md5ComparisonTests
    {
        private string TempPath { get { return Path.GetTempPath(); } }
        private string ZipFileName { get { return "test.zip"; } }
        private string ZipFilePath { get { return Path.Combine(TempPath, ZipFileName); } }
        private string ZipMd5Hash { get; set; }
        private string SubdirectoryName { get { return "test"; } }
        private string SubdirectoryPath { get { return Path.Combine(TempPath, SubdirectoryName); } }
        private string SubSubdirectoryPath { get { return Path.Combine(SubdirectoryPath, "subdir"); } }

        [TestInitialize]
        public void InitializeTest()
        {
            //create test directory and test files
            Directory.CreateDirectory(SubdirectoryPath);

            var random = new Random(System.Environment.TickCount);
            var buffer = new byte[4096];
            for (int i = 0; i < 5; i++)
            {
                var file = File.Create(Path.Combine(SubdirectoryPath, "test" + i + ".tst"));
                random.NextBytes(buffer);
                file.Write(buffer, 0, buffer.Length);
                file.Close();
            }

            Directory.CreateDirectory(SubSubdirectoryPath);
            for (int i = 0; i < 5; i++)
            {
                var file = File.Create(Path.Combine(SubSubdirectoryPath, "test" + i + ".tst"));
                random.NextBytes(buffer);
                file.Write(buffer, 0, buffer.Length);
                file.Close();
            }

            //create an example ZIP file and compute the correct MD5 hash
            ZipFile.CreateFromDirectory(SubdirectoryPath, Path.Combine(TempPath, ZipFileName));
            ZipMd5Hash = GetMd5HashFromFile(ZipFilePath);
        }

        [TestCleanup]
        public void CleanUpTest()
        {
            Directory.Delete(SubdirectoryPath, true);
            File.Delete(ZipFilePath);
        }

        private string GetMd5HashFromFile(string file)
        {
            var fileStream = File.OpenRead(file);
            var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(fileStream);
            fileStream.Close();

            return BitConverter.ToString(hash).Replace("-", "");
        }

        [TestMethod()]
        public void Md5ComparisonTestOk()
        {
            var md5Comparison = new Md5Comparison(ZipMd5Hash, ZipFilePath, SubdirectoryPath);
            Assert.AreEqual(CompareResult.Ok, md5Comparison.Compare());

            md5Comparison = new Md5Comparison(ZipMd5Hash, ZipFilePath, SubdirectoryPath, Console.Out);
            Assert.AreEqual(CompareResult.Ok, md5Comparison.Compare());

            var cliArguments = new CommandLineArguments();
            cliArguments.Md5Hash = ZipMd5Hash;
            cliArguments.ZipFilePath = ZipFilePath;
            cliArguments.CompareFolder = SubdirectoryPath;

            md5Comparison = new Md5Comparison(cliArguments);
            Assert.AreEqual(CompareResult.Ok, md5Comparison.Compare());

            md5Comparison = new Md5Comparison(cliArguments, Console.Out);
            Assert.AreEqual(CompareResult.Ok, md5Comparison.Compare());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidMd5Hash()
        {
            var md5Comparison = new Md5Comparison(ZipMd5Hash + "A", ZipFilePath, SubdirectoryPath);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Md5HashNull()
        {
            var md5Comparison = new Md5Comparison(null, ZipFilePath, SubdirectoryPath, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void InvalidZipFile()
        {
            var md5Comparison = new Md5Comparison(ZipMd5Hash, ZipFilePath + "A", SubdirectoryPath);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ZipFilePathNull()
        {
            var md5Comparison = new Md5Comparison(ZipMd5Hash, null, SubdirectoryPath, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void InvalidCompareFolder()
        {
            var md5Comparison = new Md5Comparison(ZipMd5Hash, ZipFilePath, SubdirectoryPath + "A");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DirectoryNull()
        {
            var md5Comparison = new Md5Comparison(ZipMd5Hash, ZipFilePath, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TextWriterNull()
        {
            var md5Comparison = new Md5Comparison(ZipMd5Hash, ZipFilePath, SubdirectoryPath, null);
        }

        [TestMethod()]
        public void WrongMd5Hash()
        {
            var md5Comparison = new Md5Comparison(ZipMd5Hash.Remove(ZipMd5Hash.Length - 1).Insert(0, "X"), ZipFilePath, SubdirectoryPath);
            Assert.AreEqual(CompareResult.InvalidZipHash, md5Comparison.Compare());
        }

        [TestMethod()]
        public void WrongHashForFileInDirectory()
        {
            var file = File.OpenWrite(Path.Combine(SubdirectoryPath, "test1.tst"));
            file.WriteByte(0xFF);
            file.Close();

            var md5Comparison = new Md5Comparison(ZipMd5Hash, ZipFilePath, SubdirectoryPath);
            Assert.AreEqual(CompareResult.InvalidFileHash, md5Comparison.Compare());
        }

        [TestMethod()]
        public void WrongHashForFileInSubDirectory()
        {
            var file = File.OpenWrite(Path.Combine(SubSubdirectoryPath, "test1.tst"));
            file.WriteByte(0xFF);
            file.Close();

            var md5Comparison = new Md5Comparison(ZipMd5Hash, ZipFilePath, SubdirectoryPath);
            Assert.AreEqual(CompareResult.InvalidFileHash, md5Comparison.Compare());
        }

        [TestMethod()]
        public void CorruptZipFile()
        {
            var file = File.OpenWrite(ZipFilePath);
            file.WriteByte(0xFF);
            file.Close();

            var md5Comparison = new Md5Comparison(ZipMd5Hash, ZipFilePath, SubdirectoryPath);
            Assert.AreEqual(CompareResult.InvalidZipHash, md5Comparison.Compare());
        }

        [TestMethod()]
        public void CorruptZipFileWithCorrectHash()
        {
            var file = File.OpenWrite(ZipFilePath);
            file.WriteByte(0xFF);
            file.Close();

            var md5Hash = GetMd5HashFromFile(ZipFilePath);

            var md5Comparison = new Md5Comparison(md5Hash, ZipFilePath, SubdirectoryPath);
            Assert.AreEqual(CompareResult.CorruptZipFile, md5Comparison.Compare());
        }
    }
}
