namespace Application.DTOs.ClientDto
{
    public class ClientDetailResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string BirthDate { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string UrlAttachmentCV { get; set; }
        public string UrlAttachmentProfile { get; set; }
    }
}