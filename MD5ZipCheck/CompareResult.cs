using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD5ZipFolderCheck
{
    public enum CompareResult : int
    {
        Unknown = 255,
        Ok = 0,                 // hash & files ok
        InvalidZipHash = 1,     // invalid zip file hash
        InvalidFileHash = 2,    // invalid file hash
        CorruptZipFile = 3,     // corrupt ZIP file detected
        Error = 4               // an error occurred (e.g. internal exception)
    }
}
