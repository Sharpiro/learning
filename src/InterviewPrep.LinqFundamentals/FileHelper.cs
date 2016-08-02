using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Interview.LinqFundamentals
{
    public class FileHelper
    {
        public IEnumerable<object> GetLargestFileMethodSyntax(string directory)
        {
            var largestFiles = new DirectoryInfo(directory).GetFiles()
            .OrderByDescending(fi => fi.Length).Take(5)
            .Select(fi => new { Size = fi.Length, Name = fi.Name });
            return largestFiles;
        }

        public IEnumerable<object> GetLargestFileQuerySyntax(string directory)
        {
            var largestFiles = (from file in new DirectoryInfo(directory).GetFiles()
                                orderby file.Length descending
                                select file).Take(5);
            return largestFiles;
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            var temp = "hi";
            temp.CompareTo("hi");
            return y.Length.CompareTo(x.Length);
        }
    }
}