namespace Domain.Entities
{
    public class Attachment : Base.BaseEntities<string>
    {
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public string Section { get; set; }
        public string? ReferenceId { get; set; }
    }
}
