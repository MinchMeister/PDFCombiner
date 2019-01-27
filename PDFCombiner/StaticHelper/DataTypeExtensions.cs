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
        public static bool IsPDF(this FileInfo fileInfo)
        {
            return fileInfo.Extension.ToUpper().Equals(MagicStrings.PDF);
        }

        public static bool IsTIFF(this FileInfo fileInfo)
        {
            return fileInfo.Extension.ToUpper().Equals(MagicStrings.TIFF);
        }
    }
}
