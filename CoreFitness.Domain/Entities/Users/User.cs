using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Interfaces;


namespace CoreFitness.Domain.Entities.Users
{
    internal class User : BaseEntity<UserId>, IAggregateRoot
    {
        public UserEmail Email { get; private set; }
        public string EmailUnique { get; private set; } = default!;
        public UserName UserName { get; private set; }
        public UserPhoneNumber? UserPhoneNumber { get; private set; }

        private User() { }

        public User(UserId id, UserEmail email, UserName userName, UserPhoneNumber? phoneNumber)
        {
            Id = id;
            Email = email;
            UserName = userName;
            UserPhoneNumber = phoneNumber;
        }
    }
}
