namespace UserHandling.DTOs
{
    public class PdfUploadDto
    {
        public int PdfId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public List<string> Authors { get; set; }
    }
}
