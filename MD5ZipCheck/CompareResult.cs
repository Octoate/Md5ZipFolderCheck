using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD5ZipCheck
{
    internal enum CompareResult : int
    {
        Unknown = 255,
        Ok = 0,                 // hash & files ok
        InvalidZipHash = 1,     // invalid zip file hash
        InvalidFileHash = 2,    // invalid file hash
        Error = 3               // an error occurred (e.g. internal exception)
    }
}
