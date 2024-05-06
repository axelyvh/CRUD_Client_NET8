namespace Application.DTOs.AttachmentDto
{
    public class AttachmentFileRequestDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] File { get; set; }
        public string NewFileName { get; set; }
        public string Section { get; set; }
    }
}
