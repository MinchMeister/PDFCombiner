using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFCombiner.StaticHelper
{
    public static class DataTypeExtensions
    {
        public static bool IsPDF(this FileInfo fileInfo) => fileInfo.Extension.ToUpper().Equals(Constants.PDF);
        public static bool IsPDF(this string file) => new FileInfo(file).Extension.ToUpper().Equals(Constants.PDF);

        public static bool IsTIFF(this FileInfo fileInfo) => fileInfo.Extension.ToUpper().Equals(Constants.TIFF);
        public static bool IsTIFF(this string file) => new FileInfo(file).Extension.ToUpper().Equals(Constants.TIFF);

        public static bool IsImage(this FileInfo fileInfo) => Constants.allowableImageExtensions.Contains(fileInfo.Extension.ToUpper());
        public static bool IsImage(this string file) => Constants.allowableImageExtensions.Contains(new FileInfo(file).Extension.ToUpper());

        public static bool HasTwoOrMoreFiles(this List<string> fileList) => fileList.Count() < 2;
    }
}
