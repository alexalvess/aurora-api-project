using Flunt.Notifications;

namespace Aurora.Domain.Entities
{
    public abstract class BaseEntity<TKeyType> : Notifiable
    {
        protected BaseEntity(TKeyType id = default)
        {
            Id = id;
        }

        public virtual TKeyType Id { get; }
    }
}