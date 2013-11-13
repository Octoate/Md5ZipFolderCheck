using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD5ZipFolderCheck
{
    public class CommandLineArguments
    {
        [Option("md5", Required = true, HelpText = "MD5 hash of the ZIP file.")]
        public string Md5Hash { get; set; }

        [Option('z', "zipfilepath", Required = true, HelpText = "Full path to the ZIP file.")]
        public string ZipFilePath { get; set; }

        [Option('f', "comparefolder", Required = true, HelpText = "Full path to the folder which shall be compared to the contents of the ZIP file.")]
        public string CompareFolder { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("MD5 Zip Folder Check");
            sb.AppendLine("Written 2013 by Tim Riemann");
            sb.AppendLine("http://www.octoate.de");
            sb.AppendLine("Source code: http://github.com/Octoate/Md5ZipFolderCheck");
            sb.AppendLine();
            sb.AppendLine("This utility compares a given ZIP file to a MD5 hash. After this comparison was successful, it compares the " +
                "contents of the ZIP file with the files in the given folder.");
            sb.AppendLine();
            sb.AppendLine("Usage: md5zipfoldercheck --md5 <MD5Hash> -z <full path to the ZIP file> -f <full path to compare folder>");
            sb.AppendLine();
            sb.AppendLine(@"Example: md5zipfoldercheck --md5 AABBCCEEFFGGHHIIJJKKLLMMNNOOPPQQ -z c:\test\test.zip -f c:\comparefolder");
            sb.AppendLine();
            sb.AppendLine("Returns: 0 = files and content is ok.");
            sb.AppendLine("         1 = invalid ZIP file detected");
            sb.AppendLine("         2 = invalid folder comparison");
            sb.AppendLine();

            return sb.ToString();
        }

    }
}
