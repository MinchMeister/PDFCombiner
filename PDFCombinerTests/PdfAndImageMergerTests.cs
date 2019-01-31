using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFCombiner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFCombiner.Tests
{
    [TestClass()]
    public class PdfAndImageMergerTests
    {
        [TestMethod()]
        public void CombinePdfsByDirectoryTest()
        {
            string directory = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            directory = Directory.GetParent(directory).FullName;
            directory = directory + "\\Data";

            PdfAndImageMerger.CombinePdfsByDirectory(directory);
        }








        [TestMethod()]
        public void CreatePdfsFromImagesAndPdfsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreatePdfsFromImagesAndPdfsTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AppendPdfToExistingPdfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AppendImageToExistingPdfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AppendTextToExistingPdfTest()
        {
            Assert.Fail();
        }
    }
}