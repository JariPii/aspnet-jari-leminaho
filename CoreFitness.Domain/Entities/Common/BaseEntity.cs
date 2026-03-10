using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities.Common
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; protected set; } = default!;
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
        public byte[] RowVersion { get; private set; } = default!;

        public void UpdateTimeStamp() => UpdatedAt = DateTime.UtcNow;
    }
}
