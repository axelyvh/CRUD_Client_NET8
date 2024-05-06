namespace Application.DTOs.ClientDto
{
    public class ClientUpdateRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string BirthDate { get; set; }
        public string AttachmentCV { get; set; }
        public string AttachmentProfile { get; set; }
    }
}