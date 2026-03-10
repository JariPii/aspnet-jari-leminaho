using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities.Common
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; protected set; } = default!;
        public byte[] RowVersion { get; private set; } = default!;
    }
}
