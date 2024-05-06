namespace Application.DTOs.AttachmentDto
{
    public class AttachmentDownloadFileResponseDto
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public Stream File { get; set; }
    }
}
