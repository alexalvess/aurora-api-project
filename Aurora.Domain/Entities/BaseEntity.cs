namespace Aurora.Domain.Entities
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; set; }
    }
}