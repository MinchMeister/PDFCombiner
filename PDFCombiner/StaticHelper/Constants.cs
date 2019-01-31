using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFCombiner.StaticHelper
{
    public static class Constants
    {
        public const string PDF = ".PDF";
        public const string TIFF = ".TIFF";

        public static readonly List<string> allowableImageExtensions = new List<string> { ".JPG", ".JPEG", ".PNG", ".BMP" };

    }
}
