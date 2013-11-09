using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MD5ZipCheck
{
    internal class Md5Comparison
    {
        private CommandLineArguments cliArguments;

        private string Md5Hash { get; set; }
        private string ZipFilePath { get; set; }
        private string CompareFolder { get; set; }
        private TextWriter TextWriter { get; set; }

        public Md5Comparison(string md5hash, string zipFilePath, string compareFolder, TextWriter textWriter)
        {
            Md5Hash = md5hash.Replace(" ", "").Replace("-", "");
            ZipFilePath = zipFilePath;
            CompareFolder = compareFolder;
        }

        public Md5Comparison(string md5hash, string zipFilePath, string compareFolder)
            : this(md5hash, zipFilePath, compareFolder, Console.Out)
        {
        }

        public Md5Comparison(CommandLineArguments cliArguments, TextWriter textWriter)
        {
            Md5Hash = cliArguments.Md5Hash;
            ZipFilePath = cliArguments.ZipFilePath;
            CompareFolder = cliArguments.CompareFolder;
            TextWriter = textWriter;
        }

        public Md5Comparison(CommandLineArguments cliArguments)
            : this(cliArguments, Console.Out)
        {
        }

        public CompareResult Compare()
        {
            CheckParameters();

            //compute MD5 hash for ZIP file
            string zipMd5 = GetMd5HashFromFile(ZipFilePath);
            if (!zipMd5.Equals(Md5Hash))
            {
                Console.WriteLine("Zip file MD5 mismatch: Expected MD5 hash = {0}, Computed MD5 hash = '{1}'", Md5Hash, zipMd5);
                return CompareResult.InvalidZipHash;
            }
            Console.WriteLine("Zip MD5 hash - OK.");

            //compute MD5 hashes for files in folder
            var taskGetFolderHashes = Task.Run<ConcurrentDictionary<string, string>>(() =>
                {
                    return GetFolderHashes(CompareFolder);
                });

            //computer MD5 hashes for files in ZIP file
            var taskGetZipHashes = Task.Run<ConcurrentDictionary<string, string>>(() =>
                {
                    return GetMd5HashesFromZip(ZipFilePath);
                });

            Task.WaitAll(taskGetFolderHashes, taskGetZipHashes);

            //finally compare all MD5 file hashes
            var folderHashes = taskGetFolderHashes.Result;
            var zipHashes = taskGetZipHashes.Result;

            int ok = 1;
            var loopState = Parallel.ForEach(folderHashes, (x) =>
                {
                    if (ok > 0 && zipHashes.ContainsKey(x.Key) && !zipHashes[x.Key].Equals(x.Value))
                    {
                        Interlocked.Exchange(ref ok, 0);
                        Console.WriteLine("Invalid MD5 hash for file '{0}' - expected '{1}' but was {0}.", x.Key, zipHashes[x.Key], x.Value);
                    }
                });

            if (ok == 0)
            {
                Console.WriteLine("MD5 file mismatch(es) detected.");
                return CompareResult.InvalidFileHash;
            }

            Console.WriteLine("MD5 file comparison - OK.");

            return CompareResult.Ok;
        }

        private void CheckParameters()
        {
            if (!(Md5Hash.Length == 32))
            {
                throw new ArgumentException("Invalid MD5 hash (32 character hex string needed).");
            }

            if (!File.Exists(ZipFilePath))
            {
                throw new FileNotFoundException("File not found!", ZipFilePath);
            }

            if (!Directory.Exists(CompareFolder))
            {
                throw new DirectoryNotFoundException(string.Format("Directory not found: '{0}'", CompareFolder));
            }
        }

        #region ZIP operations

        private ConcurrentDictionary<string, string> GetMd5HashesFromZip(string zipPath)
        {
            var md5Hashes = new ConcurrentDictionary<string, string>();
            var md5 = MD5.Create();
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    var fileStream = entry.Open();
                    md5.Initialize();
                    var hash = md5.ComputeHash(fileStream);
                    var hashString = BitConverter.ToString(hash).Replace("-", "");
                    fileStream.Close();

                    Console.WriteLine("ZipEntry: {0}, MD5 hash = {1}", entry.FullName, hashString);
                    md5Hashes.TryAdd(entry.FullName, hashString);
                }
            }

            return md5Hashes;
        }

        #endregion

        #region Folder operations

        private ConcurrentDictionary<string, string> GetFolderHashes(string folder)
        {
            var allFiles = GetAllFilesFromFolder(folder);

            //add seperator, if not existing, to get the correct relative file path
            if (folder[folder.Length - 1] != Path.PathSeparator)
            {
                folder += Path.DirectorySeparatorChar;
            }

            var folderHashes = new ConcurrentDictionary<string, string>();
            foreach (var file in allFiles)
            {
                var relativeFile = file.Replace(folder, "");
                var md5Hash = GetMd5HashFromFile(file);
                Console.WriteLine("File = {0}, MD5 hash = {1}", relativeFile, md5Hash);
                folderHashes.TryAdd(relativeFile, md5Hash);
            }

            return folderHashes;
        }

        private List<string> GetAllFilesFromFolder(string folder)
        {
            var allFiles = new List<string>();

            //get all files in directories and subdirectories
            var files = Directory.EnumerateFiles(folder);
            foreach (var file in files)
            {
                allFiles.Add(file);
            }

            var directories = Directory.EnumerateDirectories(folder);
            foreach (var directory in directories)
            {
                allFiles.AddRange(GetAllFilesFromFolder(directory));
            }

            return allFiles;
        }

        #endregion

        private string GetMd5HashFromFile(string file)
        {
            var fileStream = File.OpenRead(file);
            var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(fileStream);
            fileStream.Close();

            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
