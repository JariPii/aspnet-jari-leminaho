using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Interfaces;


namespace CoreFitness.Domain.Entities.Users
{
    internal class User : BaseEntity<UserId>, IAggregateRoot
    {
        public UserEmail Email { get; private set; }
        public string EmailUnique => Email.UniqueValue;
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

        public void UpdateEmail(UserEmail newUserEmail)
        {
            if (Email == newUserEmail) return;

            Email = newUserEmail;
            UpdateTimeStamp();
        }
    }
}
