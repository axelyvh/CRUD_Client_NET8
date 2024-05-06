namespace Application.DTOs.ClientDto
{
    public class ClientInboxResponseDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthDateFormat { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
    }
}