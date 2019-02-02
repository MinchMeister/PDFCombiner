namespace PDFCombiner
{
    public interface IAPI
    {
        string CombinePdfsByDirectory(string directory, string combinedFileName);  //json as input for API
    }
}