namespace CoreFitness.Domain.Entities.Common
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; protected set; } = default!;
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public byte[] RowVersion { get; private set; } = default!;

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateTimeStamp() => UpdatedAt = DateTime.UtcNow;
    }
}
