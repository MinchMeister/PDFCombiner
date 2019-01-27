using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace PDFCombiner
{
    public class PdfAndImageMerger
    {
        public enum OrderFilesByOptions { NAME, CREATION_TIME, MODIFIED_DATE };
        static readonly int PageMarginTop = 25;
        static readonly int PageMarginLeft = 25;
        static readonly int PageBodyWidth = 550;


        public static void CreatePdfsFromImagesAndPdfs(string outputPdfFullName, string pathToImagesAndPdfs)
        {
            CreatePdfsFromImagesAndPdfs(outputPdfFullName, pathToImagesAndPdfs, ".*");
        }
        public static void CreatePdfsFromImagesAndPdfs(string outputPdfFullName, string pathToImagesAndPdfs, string fileNamePattern)
        {
            if (File.Exists(outputPdfFullName))
            {
                File.Delete(outputPdfFullName);
            }
            PdfDocument document = new PdfDocument();

            //Get All the File Names in the directory
            List<string> pathsToImagesAndPdfs = new List<string>();
            pathsToImagesAndPdfs.AddRange(FilterImagesAndPdfsInADirectory(pathToImagesAndPdfs, fileNamePattern, OrderFilesByOptions.MODIFIED_DATE));

            //If the output PDF already exists, you dont want to recombine it with itself
            var outputPdfFullNameItem = pathsToImagesAndPdfs.Where(path => (new FileInfo(path)).FullName.ToLower() == outputPdfFullName.ToLower()).FirstOrDefault();
            pathsToImagesAndPdfs.Remove(outputPdfFullNameItem);

            //Sort them however you want

            //If a file leads to a PDF, Add it
            //If a file leads to an image, keep loading image paths until you run out of files or run into a pdf. Then, Add all those images at the same time, via list

            List<string> currentSegmentOfConsecutiveImagePaths = new List<string>();
            foreach (string pathToImageOrPdf in pathsToImagesAndPdfs)
            {
                if (FileIsAPdf(pathToImageOrPdf))
                {
                    //DUPLICATE CODE
                    if (currentSegmentOfConsecutiveImagePaths.Count > 0)
                    {
                        document = AddImagesToPdfDocument(document, currentSegmentOfConsecutiveImagePaths);
                        currentSegmentOfConsecutiveImagePaths.Clear();
                    }
                    document = AddPdfToPdfDocument(document, pathToImageOrPdf);
                }
                if (FileIsATiff(pathToImageOrPdf))
                {
                    document = AddTiffToPdfDocument(document, pathToImageOrPdf);
                }

                if (FileIsAnImage(pathToImageOrPdf))
                {
                    currentSegmentOfConsecutiveImagePaths.Add(pathToImageOrPdf);
                }
            }
            //DUPLICATE CODE
            if (currentSegmentOfConsecutiveImagePaths.Count > 0)
            {
                document = AddImagesToPdfDocument(document, currentSegmentOfConsecutiveImagePaths);
                currentSegmentOfConsecutiveImagePaths.Clear();
            }

            if (outputPdfFullName.Length == 0)
            {
                throw new SystemException("Must Specify Target Directory for new PDF");
            }

            //If Output already exists, override it.


            document.Save(outputPdfFullName);
            //Process.Start(outputPdfFullName);
        }
        public static void AppendPdfToExistingPdf(string pathToPdf, string pathToNewPdf) { }
        public static void AppendImageToExistingPdf(string pathToPdf, Image imageToAdd)
        {
            int x = PageMarginLeft;
            int y = PageMarginTop;
            int width = PageBodyWidth;
            int height = 250;

            PdfDocument document = PdfReader.Open(pathToPdf, PdfDocumentOpenMode.Modify);
            PdfPage page = document.AddPage();
            XImage image = imageToAdd;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            gfx.DrawImage(image, x, y, width, height);
            document.Save(pathToPdf);
            document.Close();
        }
        public static void AppendTextToExistingPdf(string pathToPdf, string textToAdd) { }

        private static PdfDocument AddTiffToPdfDocument(PdfDocument document, string pathToTiff)
        {
            int activePage;
            int pages;
            Image image = Image.FromFile(pathToTiff);
            pages = image.GetFrameCount(FrameDimension.Page);
            for (int index = 0; index < pages; index++)
            {
                activePage = index + 1;
                image.SelectActiveFrame(FrameDimension.Page, index);
                PdfPage page = document.AddPage();
                page.TrimMargins.All = 5;
                page.Size = PdfSharp.PageSize.Letter;
                XImage xImage = image;
                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(xImage, PageMarginLeft - 10, PageMarginTop - 10, 550, 700);
            }
            return document;
        }
        private static PdfDocument AddImagesToPdfDocument(PdfDocument document, List<string> imagePaths)
        {
            int maxImagesOnPage = 3;
            int x = PageMarginLeft;
            int y = PageMarginTop;
            int width = PageBodyWidth;
            int height = 250;
            int marginTop = 5;
            int imageIndex = 0;
            PdfPage page = document.AddPage();
            XImage image;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            foreach (string imagePath in imagePaths)
            {
                if (imageIndex == maxImagesOnPage)
                {
                    imageIndex = 0;
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                }
                y = PageMarginTop + (height + marginTop) * imageIndex;
                image = XImage.FromFile(imagePath);
                gfx.DrawImage(image, x, y, width, height);
                imageIndex++;
            }
            return document;
        }
        private static PdfDocument AddPdfToPdfDocument(PdfDocument document, string pathToPdf)
        {
            PdfDocument pdfToAppendToDocument = PdfReader.Open(pathToPdf, PdfDocumentOpenMode.Import);
            for (int oldPageIndex = 0; oldPageIndex < pdfToAppendToDocument.Pages.Count; oldPageIndex++)
            {
                PdfPage pp = document.AddPage(pdfToAppendToDocument.Pages[oldPageIndex]);
            }
            return document;
        }
        private static List<string> FilterImagesAndPdfsInADirectory(string pathToDirectory, string fileNamePattern, OrderFilesByOptions orderFilesBy)
        {
            List<string> fullNames = new List<string>();
            if (File.Exists(pathToDirectory))
            {
                fullNames.Add(pathToDirectory);
                return fullNames;
            }
            else if (Directory.Exists(pathToDirectory))
            {
                switch (orderFilesBy)
                {
                    case OrderFilesByOptions.MODIFIED_DATE:
                        fullNames.AddRange(GetFileFullNamesInDirectoryByModifiedDate(pathToDirectory));
                        break;
                    case OrderFilesByOptions.CREATION_TIME:
                        fullNames.AddRange(GetFileFullNamesInDirectoryByCreationTime(pathToDirectory));
                        break;
                    case OrderFilesByOptions.NAME:
                        fullNames.AddRange(GetFileFullNamesInDirectoryByName(pathToDirectory));
                        break;
                    default:
                        fullNames.AddRange(GetFileFullNamesInDirectoryByCreationTime(pathToDirectory));
                        break;

                }
                //Filter By name. useful for selecting only "legal documens, sig cards, etc"
                fullNames = fullNames.Where(f => Regex.IsMatch((new FileInfo(f)).Name, fileNamePattern)).ToList();
                return fullNames;
            }
            return fullNames; //Not sure if its better to throw an error, or to return an empty list.

        }
        private static List<string> GetFileFullNamesInDirectoryByCreationTime(string pathToDirectory)
        {
            List<string> fullNames = new List<string>();
            fullNames.AddRange(Directory.EnumerateFiles(pathToDirectory)
                    .OrderBy(f => new FileInfo(f).CreationTime)
            );
            return fullNames;
        }
        private static List<string> GetFileFullNamesInDirectoryByModifiedDate(string pathToDirectory)
        {
            List<string> fullNames = new List<string>();
            fullNames.AddRange(Directory.EnumerateFiles(pathToDirectory)
                    .OrderBy(f => new FileInfo(f).LastWriteTime)
            );
            return fullNames;
        }
        private static List<string> GetFileFullNamesInDirectoryByName(string pathToDirectory)
        {
            List<string> fullNames = new List<string>();
            fullNames.AddRange(Directory.EnumerateFiles(pathToDirectory)
                    .OrderBy(f => new FileInfo(f).Name)
            );
            return fullNames;
        }
        private static bool FileIsAPdf(string fileFullName)
        {
            return (new FileInfo(fileFullName)).Extension.ToLower().Contains("pdf");
        }
        private static bool FileIsATiff(string fileFullName)
        {
            return (new FileInfo(fileFullName)).Extension.ToLower().Contains("tif");
        }
        private static bool FileIsAnImage(string imageFullName)
        {
            List<string> allowableImageExtensions = new List<string> { ".jpg", ".png", ".bmp", ".jpeg" };
            return allowableImageExtensions.Contains(Path.GetExtension(imageFullName));

        }

    }
}
