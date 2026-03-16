using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Interfaces;


namespace CoreFitness.Domain.Entities.Users
{
    internal class User : BaseEntity<UserId>, IAggregateRoot
    {
        private UserEmail _email; 
        public UserEmail Email
        {
            get => _email;
            private set
            {
                _email = value;
                EmailUnique = value.UniqueValue;
            }
        }
        public string EmailUnique { get; private set; } = default!;
        public UserName UserName { get; private set; }
        public UserPhoneNumber? UserPhoneNumber { get; private set; }

        public static User Create(UserEmail email, UserName name, UserPhoneNumber? phonenumber)
            => new(UserId.New(), email, name, phonenumber);

        protected User(UserId id, UserEmail email, UserName userName, UserPhoneNumber? phoneNumber)
        {
            Id = id;
            Email = email;
            UserName = userName;
            UserPhoneNumber = phoneNumber;
        }
        protected User() { }
    }
}
