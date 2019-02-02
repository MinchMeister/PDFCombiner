using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PDFCombiner.StaticHelper
{
    public static class DirectoryWrapper
    {
        public static bool DoesDirectoryExist(string directory) => Directory.Exists(directory);

        public static List<string> GetAllFilesInDirectory(string directory) => Directory.GetFiles(directory).ToList();

        public static List<string> GetAllPdfFilesInDirectory(string directory) => Directory.GetFiles(directory, "*.pdf").ToList(); //if .pdf is uppercase this will be an issue
    }
}