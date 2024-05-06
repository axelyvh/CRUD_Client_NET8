namespace Domain.Entities.Base
{
    public abstract class BaseEntities<T>
    {
        public T Id { get; set; }
        public bool State { get; set; }
    }
}