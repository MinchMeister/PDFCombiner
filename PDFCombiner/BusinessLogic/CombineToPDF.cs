using PDFCombiner.StaticHelper;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;

namespace PDFCombiner.BusinessLogic
{
    public class CombineToPDF
    {
        public PdfDocument MergePDFs(List<string> fileList)
        {
            PdfDocument outputDocument = new PdfDocument();

            foreach (var file in fileList)
            {
                if (file.IsPDF()) //can strip this out if you can guarantee that the files are only PDFs
                {
                    PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                    int pageCount = inputDocument.PageCount;

                    for (int idx = 0; idx < pageCount; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                    }
                }
            }
            return outputDocument;
        }

    }
}