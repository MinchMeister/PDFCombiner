using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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

            PdfAndImageMerger pdfAndImageMerger = new PdfAndImageMerger();

            var result = pdfAndImageMerger.CombinePdfsByDirectory(directory, "CombinedFileYAY");

            Assert.IsNotNull(result);
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



        [TestMethod()]
        public void CombinePdfsByDirectoryTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreatePdfsFromImagesAndPdfsTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreatePdfsFromImagesAndPdfsTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AppendPdfToExistingPdfTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AppendImageToExistingPdfTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AppendTextToExistingPdfTest1()
        {
            Assert.Fail();
        }
    }
}