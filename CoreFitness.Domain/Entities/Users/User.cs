using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Users.ValueObjects;


namespace CoreFitness.Domain.Entities.Users
{
    internal class User : BaseEntity<UserId>
    {
        public UserId UserId { get; private set; }
    }
}
