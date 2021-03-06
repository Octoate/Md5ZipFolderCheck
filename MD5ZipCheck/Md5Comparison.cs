﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MD5ZipFolderCheck
{
    public class Md5Comparison
    {
        private string Md5Hash { get; set; }
        private string ZipFilePath { get; set; }
        private string CompareFolder { get; set; }
        private TextWriter TextWriter { get; set; }

        public Md5Comparison(string md5hash, string zipFilePath, string compareFolder, TextWriter textWriter)
        {
            if (md5hash == null)
            {
                throw new ArgumentNullException("md5hash", "Invalid MD5 hash.");
            }

            Md5Hash = md5hash.Replace(" ", "").Replace("-", "").ToUpper();

            if (!(Md5Hash.Length == 32))
            {
                throw new ArgumentException("Invalid MD5 hash (32 character hex string needed).");
            }

            if (zipFilePath == null)
            {
                throw new ArgumentNullException("zipFilePath", "Invalid ZIP file path.");
            }

            if (!File.Exists(zipFilePath))
            {
                throw new FileNotFoundException("ZIP file not found: '{0}'", ZipFilePath);
            }

            if (compareFolder == null)
            {
                throw new ArgumentNullException("compareFolder", "Invalid compare folder path.");
            }

            if (!Directory.Exists(compareFolder))
            {
                throw new DirectoryNotFoundException(string.Format("Directory not found: '{0}'", CompareFolder));
            }

            if (textWriter == null)
            {
                throw new ArgumentNullException("textWriter");
            }

            ZipFilePath = zipFilePath;
            CompareFolder = compareFolder.TrimEnd('\\'); //trim any trailing backslash
            TextWriter = textWriter;
        }

        public Md5Comparison(string md5hash, string zipFilePath, string compareFolder)
            : this(md5hash, zipFilePath, compareFolder, Console.Out)
        {
        }

        public Md5Comparison(CommandLineArguments cliArguments, TextWriter textWriter)
            : this(cliArguments.Md5Hash, cliArguments.ZipFilePath, cliArguments.CompareFolder, textWriter)
        {
        }

        public Md5Comparison(CommandLineArguments cliArguments)
            : this(cliArguments, Console.Out)
        {
        }

        public CompareResult Compare()
        {
            //compute MD5 hash for ZIP file
            string zipMd5 = GetMd5HashFromFile(ZipFilePath);
            if (!zipMd5.Equals(Md5Hash))
            {
                Console.WriteLine("Zip file MD5 mismatch: Expected MD5 hash = {0}, Computed MD5 hash = '{1}'", Md5Hash, zipMd5);
                return CompareResult.InvalidZipHash;
            }
            TextWriter.WriteLine("Zip MD5 hash - OK.");

            Task<ConcurrentDictionary<string, string>> taskGetFolderHashes = null;
            Task<ConcurrentDictionary<string, string>> taskGetZipHashes = null;
            try
            {
                //compute MD5 hashes for files in folder
                taskGetFolderHashes = Task.Run<ConcurrentDictionary<string, string>>(() =>
                    {
                        return GetFolderHashes(CompareFolder);
                    });

                
                //compute MD5 hashes for files in ZIP file
                taskGetZipHashes = Task.Run<ConcurrentDictionary<string, string>>(() =>
                    {
                        return GetMd5HashesFromZip(ZipFilePath);
                    });

                Task.WaitAll(taskGetFolderHashes, taskGetZipHashes); //-> AggregateException from threads will be thrown here
            }
            catch (AggregateException ae)
            {
                //-> corrupt ZIP file
                return CompareResult.CorruptZipFile;
            }



            //finally compare all MD5 file hashes
            var folderHashes = taskGetFolderHashes.Result;
            var zipHashes = taskGetZipHashes.Result;

            int ok = 1;
            var loopState = Parallel.ForEach(folderHashes, (x) =>
                {
                    if (ok > 0 && zipHashes.ContainsKey(x.Key) && !zipHashes[x.Key].Equals(x.Value))
                    {
                        Interlocked.Exchange(ref ok, 0);
                        TextWriter.WriteLine("Invalid MD5 hash for file '{0}' - expected '{1}' but was '{2}'.", x.Key, zipHashes[x.Key], x.Value);
                    }
                });

            if (ok == 0)
            {
                TextWriter.WriteLine("MD5 file mismatch(es) detected.");
                return CompareResult.InvalidFileHash;
            }

            TextWriter.WriteLine("MD5 file comparison - OK.");

            return CompareResult.Ok;
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

                    TextWriter.WriteLine("ZipEntry: {0}, MD5 hash = {1}", entry.FullName, hashString);
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
                TextWriter.WriteLine("File = {0}, MD5 hash = {1}", relativeFile, md5Hash);
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
