namespace Domain.Entities
{
    public class Client : Base.BaseEntities<int>
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
    }
}
