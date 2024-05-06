namespace Application.DTOs.ClientDto
{
    public class ClientCreateRequestDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string BirthDate { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string AttachmentCV { get; set; }
        public string AttachmentProfile { get; set; }
    }
}
