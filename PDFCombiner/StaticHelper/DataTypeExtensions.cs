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
        public static bool IsPDF(this string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            return fileInfo.Extension.ToUpper().Equals(Constants.PDF);
        }
        public static bool IsPDF(this FileInfo fileInfo)
        {
            return fileInfo.Extension.ToUpper().Equals(Constants.PDF);
        }


        public static bool IsTIFF(this string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            return fileInfo.Extension.ToUpper().Equals(Constants.TIFF);
        }
        public static bool IsTIFF(this FileInfo fileInfo)
        {
            return fileInfo.Extension.ToUpper().Equals(Constants.TIFF);
        }


        public static bool IsImage(this string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            return Constants.allowableImageExtensions.Contains(fileInfo.Extension.ToUpper());
        }
        public static bool IsImage(this FileInfo fileInfo)
        {
            return Constants.allowableImageExtensions.Contains(fileInfo.Extension.ToUpper());
        }
    }
}
